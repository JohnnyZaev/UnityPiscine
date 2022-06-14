using UnityEngine;

public class DeathCondition : MonoBehaviour
{
	private void Update()
	{
		if (transform.position.y <= -5.5f)
			gameObject.GetComponent<playerScript_ex01>().IsActive = false;
	}
}
