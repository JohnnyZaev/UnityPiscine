using UnityEngine;

public class MouseMovement : MonoBehaviour
{

	private MovePosition _movePosition;
	private Camera _camera;

	private void Awake()
	{
		_camera = Camera.main;
		_movePosition = GetComponent<MovePosition>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_movePosition.SetMovePosition(_camera.ScreenToWorldPoint(Input.mousePosition));
		}
	}
}
