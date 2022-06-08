using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
	[SerializeField] private float minSpeed;
	[SerializeField] private float maxSpeed;
	[SerializeField] private KeyCode key;
	[SerializeField] private Transform endpoint;

	private float _speed;
	private void Awake()
	{
		_speed = Random.Range(minSpeed, maxSpeed);
	}

	private void Update()
	{
		if (Input.GetKeyDown(key) && transform.position.y > endpoint.position.y)
		{
			Debug.Log($"Precision: {transform.position.y - endpoint.position.y}");
			Destroy(gameObject);
		}
		transform.Translate(_speed * Time.deltaTime * Vector3.down);
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
