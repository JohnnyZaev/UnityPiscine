using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> _enemys;
    
    public bool isEnemy;

    public GameObject enemyCheckPointUp;

    public PlayerController pc;

    private void Update()
    {
        if (!isEnemy)
        {
            GameObject go = Instantiate(_enemys[Random.Range(0, _enemys.Count)], transform.position, Quaternion.identity);
            EnemyController em = go.GetComponent<EnemyController>();
            em._parent = gameObject;
            for (int i = 1; i < pc.level; i++)
            {
                em.level += 1;
                em.upgradeStat();
            }
            isEnemy = true;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawIcon(transform.position, "death.png", false);
    }
}
