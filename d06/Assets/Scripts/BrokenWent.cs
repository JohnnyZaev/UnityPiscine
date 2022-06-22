using UnityEngine;

public class BrokenWent : MonoBehaviour
{
	[SerializeField] private ParticleSystem smoke;
	private bool _isPlayerHere;
	[SerializeField] private LightTrigger cameraForWent;


	private void Update()
	{
		if (_isPlayerHere && Input.GetKeyDown(KeyCode.E))
		{
			smoke.Play();
			if (cameraForWent != null)
				Destroy(cameraForWent.gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
			_isPlayerHere = true;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
			_isPlayerHere = true;
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
			_isPlayerHere = false;
	}
}
