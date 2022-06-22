using UnityEngine;

public class LightTrigger : MonoBehaviour
{
	[SerializeField] private DetectLight lightDetector;

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
			lightDetector.currentDetection += 151 * Time.deltaTime;
	}
}
