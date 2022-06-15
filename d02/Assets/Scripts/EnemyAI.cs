using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	[SerializeField] private float acceptableDistanceToTownhall;
	[SerializeField] private float acceptableDistanceToUnit;
    
	[SerializeField] private Transform myTownhall;
	[SerializeField] private Transform enemyTownhall;
	private void Update()
	{
		if (!enemyTownhall || !myTownhall)
			return;
        
		var allUnits = FindObjectsOfType<UnitRTS>();
        
		var myUnits = allUnits.Where(x => ((1 << x.gameObject.layer) & LayerMask.GetMask("EnemyUnit")) != 0).ToList();
		var enemyUnits = allUnits.Where(x => ((1 << x.gameObject.layer) & LayerMask.GetMask("Unit")) != 0).ToList();

		foreach (var enemyUnit in enemyUnits)
		{
			if (!((enemyUnit.transform.position - myTownhall.position).sqrMagnitude <=
			      acceptableDistanceToTownhall * acceptableDistanceToTownhall) ||
			    !enemyUnit.TryGetComponent<HealthSystem>(out var health)) continue;
			myUnits.ForEach(x => x.UpdateTarget(health));
			return;
		}

		if (enemyTownhall.TryGetComponent<HealthSystem>(out var target))
			myUnits.ForEach(x =>
			{
				var closestEnemy = enemyUnits.FirstOrDefault(y => (y.transform.position - x.transform.position).sqrMagnitude <= acceptableDistanceToUnit * acceptableDistanceToUnit);
				if (closestEnemy && closestEnemy.TryGetComponent<HealthSystem>(out var enemyTarget))
				{
					x.UpdateTarget(enemyTarget);
				}
				else
				{
					x.UpdateTarget(target);
				}
			});
	}
}
