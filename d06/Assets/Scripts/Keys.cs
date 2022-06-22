using UnityEngine;

public class Keys : MonoBehaviour
{
	private bool _isPlayerHere;


	private void Update()
	{
		if (!_isPlayerHere)
			return;
		if (Input.GetKeyDown(KeyCode.E))
		{
			PlayerInventory.Instance.HasKeys = true;
			Destroy(gameObject);
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
