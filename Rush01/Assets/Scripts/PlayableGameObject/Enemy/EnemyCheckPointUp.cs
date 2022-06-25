using UnityEngine;

public class EnemyCheckPointUp : MonoBehaviour
{
	private void OnDrawGizmos() {
        Gizmos.DrawIcon(transform.position, "enemy.png", false);
    }
}
