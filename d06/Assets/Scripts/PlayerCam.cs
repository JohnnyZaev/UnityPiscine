using UnityEngine;

public class PlayerCam : MonoBehaviour
{
	[SerializeField] private float sensX;
	[SerializeField] private float sensY;
	[SerializeField] private Transform orientation;
	private float _mouseX;
	private float _mouseY;
	private float _rotationX;
	private float _rotationY;
	
	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		_mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
		_mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
		
		_rotationY += _mouseX;
		_rotationX -= _mouseY;
		_rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
		
		transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0f);
		orientation.rotation = Quaternion.Euler(0f, _rotationY, 0f);
	}
}
