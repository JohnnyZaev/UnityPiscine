using UnityEngine;

public class MouseClickPoint : MonoBehaviour
{
	private void OnDrawGizmos() {
        Gizmos.DrawIcon(transform.position, "enemy.png", false);
    }
    
}
