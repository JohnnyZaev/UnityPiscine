using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField] private Transform playerHead;

	private void Update()
	{
		transform.position = playerHead.position;
	}
}
