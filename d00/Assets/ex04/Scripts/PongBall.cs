using UnityEngine;
using Random = UnityEngine.Random;

public class PongBall : MonoBehaviour
{
	[SerializeField] private Transform[] leftPlayerCollisions;
	[SerializeField] private Transform[] rightPlayerCollisions;
	[SerializeField] private Transform[] wallsCollisions;
	[SerializeField] private float speed;
	private const int LeftWallCollision = 0;
	private const int RightWallCollision = 1;
	private const int TopWallCollision = 2;
	private const int BottomWallCollision = 3;
	private const int HitPlayerCollision = 0;
	private const int TopPlayerCollision = 1;
	private const int BottomPlayerCollision = 2;
	private const int EndHitPlayerCollision = 3;
	private int _horizontalDirection;
	private int _verticalDirection;
	private int _leftPlayerScore;
	private int _rightPlayerScore;
	private Vector3 _startingPosition;

	private void Awake()
	{
		_leftPlayerScore = 0;
		_rightPlayerScore = 0;
		PrintScore();
		_startingPosition = transform.position;
		RandomizeDirection(ref _horizontalDirection);
		RandomizeDirection(ref _verticalDirection);
	}

	private void Update()
	{
		if (transform.position.y >= wallsCollisions[TopWallCollision].position.y || transform.position.y <=
		    wallsCollisions[BottomWallCollision].position.y)
		{
			_verticalDirection *= -1;
			transform.Translate(Vector3.up * (_verticalDirection * 0.1f));
		}
		if (transform.position.x <= wallsCollisions[LeftWallCollision].position.x || transform.position.x >=
		    wallsCollisions[RightWallCollision].position.x)
		{
			Goal();
		}
		CheckCollisions();
		transform.Translate(Vector3.up * (speed * _verticalDirection * Time.deltaTime));
		transform.Translate(Vector3.right * (speed * _horizontalDirection * Time.deltaTime));
	}

	private void CheckCollisions()
	{
		if (transform.position.x > 0)
		{
			if (!(transform.position.x >= rightPlayerCollisions[HitPlayerCollision].position.x && transform.position
			.x <= rightPlayerCollisions[EndHitPlayerCollision].position.x)) return;
			if (!(transform.position.y <= rightPlayerCollisions[TopPlayerCollision].position.y) || !(transform
				    .position.y >= rightPlayerCollisions[BottomPlayerCollision].position.y)) return;
			_horizontalDirection *= -1;
			transform.Translate(Vector3.up * (_verticalDirection * 0.1f));
			transform.Translate(Vector3.right * (_horizontalDirection * 0.1f));
		}
		else
		{
			if (!(transform.position.x <= leftPlayerCollisions[HitPlayerCollision].position.x && transform.position.x
			 >= leftPlayerCollisions[EndHitPlayerCollision].position.x)) return;
			if (!(transform.position.y <= leftPlayerCollisions[TopPlayerCollision].position.y) || !(transform
				    .position.y >= leftPlayerCollisions[BottomPlayerCollision].position.y)) return;
			_horizontalDirection *= -1;
			transform.Translate(Vector3.up * (_verticalDirection * 0.1f));
			transform.Translate(Vector3.right * (_horizontalDirection * 0.1f));
		}
	}
	
	private void Goal()
	{
		if (transform.position.x > 0)
			_leftPlayerScore += 1;
		else
			_rightPlayerScore += 1;
		PrintScore();
		transform.position = _startingPosition;
		RandomizeDirection(ref _horizontalDirection);
		RandomizeDirection(ref _verticalDirection);
	}

	private static void RandomizeDirection(ref int direction)
	{
		direction = Random.Range(0, 2) * 2 - 1;
	}

	private void PrintScore()
	{
		Debug.Log($"Player 1: {_leftPlayerScore} | Player 2: {_rightPlayerScore}");
	}
}
