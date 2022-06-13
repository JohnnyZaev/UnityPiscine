using UnityEngine;

public class Teleport : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
	    col.transform.position = gameObject.transform.GetChild(0).gameObject.transform.position;
    }
}
