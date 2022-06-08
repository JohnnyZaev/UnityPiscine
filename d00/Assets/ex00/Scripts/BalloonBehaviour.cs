using UnityEngine;

public class BalloonBehaviour : MonoBehaviour
{
	[SerializeField] private float balloonStarterSize;
	[SerializeField] private float balloonMinSize;
	[SerializeField] private float balloonMaxSize;
	[SerializeField] private float breathSpeed;
	[SerializeField] private float breathDecreaseSpeed;
	[SerializeField] private float deflateSpeed;
	[SerializeField] private float breathMaxSize;
	[SerializeField] private float inflateValue;
	private float _currentBalloonSize;
	private Transform _balloonTransform;
	private bool _gameOver;
	private float _breathSize;

	private void Awake()
	{
		_balloonTransform = transform;
		_balloonTransform.localScale = balloonStarterSize * Vector3.one;
		_breathSize = breathMaxSize;
	}

	private void Update()
	{
		if (_gameOver)
			return;
		_currentBalloonSize = transform.localScale.x;
		if (_currentBalloonSize >= balloonMaxSize || _currentBalloonSize <= balloonMinSize)
		{
			Destroy(gameObject);
			Debug.Log($"Balloon lifetime: {Mathf.RoundToInt(Time.timeSinceLevelLoad)}s");
			_gameOver = true;
		}
		if (Input.GetKeyDown(KeyCode.Space) && _breathSize > 0)
		{
			_balloonTransform.localScale += inflateValue * _breathSize * Vector3.one;
			_breathSize -= breathDecreaseSpeed;
		}
		_balloonTransform.localScale -= deflateSpeed * Time.deltaTime * Vector3.one;
		if (_breathSize < breathMaxSize)
			_breathSize += breathSpeed * Time.deltaTime;
		
	}
	
}
