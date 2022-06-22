using System;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
	[SerializeField] private DetectLight light;

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
			light.currentDetection += 51 * Time.deltaTime;
	}
}
