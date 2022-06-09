using UnityEngine;

public class Bird : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private Transform groundCollision;
	[HideInInspector] public bool gameActive;
	[SerializeField] private float fallSpeed;
	public float PipeSpeed { get; set; }
	public Transform[] pipe1Collisions;
	public Transform[] pipe2Collisions;
	private bool _goingUp;
	private float _goingUpTime;
	private float _currentGoingUpTime;
	private const int StartCollision = 0;
	private const int EndCollision = 1;
	private const int MaxCollision = 2;
	private const int MinCollision = 3;
	public float Score { set; get; }

	private void Awake()
	{
		Score = 0;
		_goingUpTime = 0.5f;
		gameActive = true;
	}

	private void Update()
	{
		if (!gameActive)
			return;
		if (Input.GetKeyDown(KeyCode.Space) && !_goingUp)
		{
			_goingUp = true;
			_currentGoingUpTime = 0;
		}

		if (transform.position.x >= pipe1Collisions[StartCollision].position.x && transform.position.x <= 
		pipe1Collisions[EndCollision].position.x)
			CheckCollisions(pipe1Collisions);
		else if (transform.position.x >= pipe2Collisions[StartCollision].position.x && transform.position.x <= 
		         pipe2Collisions[EndCollision].position.x)
			CheckCollisions(pipe2Collisions);
		if (transform.position.y <= groundCollision.position.y)
		{
			gameActive = false;
			Debug.Log($"Score: {Score}\nTime: {Mathf.RoundToInt(Time.timeSinceLevelLoad)}");
		}
		if (_goingUp)
		{
			transform.Translate(Vector3.up * (speed * Time.deltaTime));
			_currentGoingUpTime += Time.deltaTime;
			if (_currentGoingUpTime >= _goingUpTime)
				_goingUp = false;
		}

		transform.Translate(Vector3.down * (Time.deltaTime * fallSpeed));
	}

	private void CheckCollisions(Transform[] collisions)
	{
		if (transform.position.y >= collisions[MaxCollision].position.y || transform.position.y <=
		    collisions[MinCollision].position.y)
		{
			gameActive = false;
			Debug.Log($"Score: {Score}\nTime: {Mathf.RoundToInt(Time.timeSinceLevelLoad)}");
		}
	}
}
