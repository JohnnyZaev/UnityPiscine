using UnityEngine;

public class Bullet : MonoBehaviour
{
	private float _speed;
	private Vector3 _direction;
	private bool _isActive;

	private void Update()
	{
		if (!_isActive)
			return;
		transform.position += _direction.normalized * (Time.deltaTime * _speed);
	}

	private void OnCollisionEnter2D()
	{
		_isActive = false;
		Destroy(gameObject);
	}

	public void Construct(float speed, Vector3 direction)
	{
		_speed = speed;
		_direction = direction;
		_isActive = true;
	}
	
	public void Construct(float speed, Vector3 direction, int layer, Color color)
	{
		gameObject.layer = layer;
		gameObject.GetComponent<SpriteRenderer>().color = color;
		_speed = speed;
		_direction = direction;
		_isActive = true;
	}
}
