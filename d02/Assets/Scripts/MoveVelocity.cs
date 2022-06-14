using UnityEngine;

public class MoveVelocity : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 250f;

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
		Debug.Log((_velocityVector - transform.position).magnitude);
		if ((_velocityVector - transform.position.normalized).sqrMagnitude < 0.2f)
		{
			_rigidbody2D.velocity = Vector2.zero;
			return;
		}

		_rigidbody2D.velocity = _velocityVector * moveSpeed;

	}
}
