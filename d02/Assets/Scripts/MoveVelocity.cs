using UnityEngine;

public class MoveVelocity : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 3f;

	private Vector3 _velocityVector;
	private Rigidbody2D _rigidbody2D;

	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
		_velocityVector = _rigidbody2D.velocity;
	}

	public void SetVelocity(Vector3 velocityVector)
	{
		_velocityVector = velocityVector;
	}

	private void FixedUpdate()
	{
		_rigidbody2D.velocity = _velocityVector * moveSpeed;
	}
}
