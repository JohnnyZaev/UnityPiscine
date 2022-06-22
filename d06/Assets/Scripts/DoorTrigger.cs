using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
	private bool _isPlayerHere;
	private bool _isOpening;
	[SerializeField] private Transform door;
	[SerializeField] private MeshRenderer doorIcon;
	[SerializeField] private Material iconToChange;


	private void Update()
	{
		if (_isOpening)
		{
			if (door.transform.position.y < 0)
			{
				door.transform.Translate(Vector3.up * Time.deltaTime);
			}
		}
		if (!_isPlayerHere)
			return;
		if (Input.GetKeyDown(KeyCode.E) && PlayerInventory.Instance.HasKeys)
		{
			OpenDoor();
			doorIcon.material = iconToChange;
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

	private void OpenDoor()
	{
		_isOpening = true;
	}
}
