using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
	[SerializeField] private float maxHealthPoints;

	[SerializeField] private UnityEvent onTakeDamage;
	[SerializeField] private UnityEvent onDeath;
    
	private float _currentHealthPoints;

	private void Start()
	{
		_currentHealthPoints = maxHealthPoints;
	}

	public void TakeDamage(float damage)
	{
		_currentHealthPoints -= damage;
		Debug.Log($"{name} [{_currentHealthPoints}/{maxHealthPoints}]HP has been attacked");
        
		onTakeDamage?.Invoke();

		if (!(_currentHealthPoints <= 0)) return;
		onDeath?.Invoke();
		Destroy(gameObject);
	}
	
}
