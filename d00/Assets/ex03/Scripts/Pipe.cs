using UnityEngine;

public class Pipe : MonoBehaviour
{
	[SerializeField] private float starterSpeed;
	[SerializeField] private float speedIncrease;
	[SerializeField] private Bird birdScript;
	[SerializeField] private Transform birdTransform;
	private Vector3 _startingPosition;
	private bool _passed;
	private float _scorePoints;

	private void Awake()
	{
		birdScript.PipeSpeed = starterSpeed;
		_startingPosition = transform.position;
		_scorePoints = 5;
	}

	private void Update()
	{
		if (!birdScript.gameActive)
			return;
		transform.Translate(Vector3.left * (Time.deltaTime * birdScript.PipeSpeed));
		if (!(birdTransform.position.x > transform.position.x) || _passed) return;
		birdScript.Score += _scorePoints;
		_passed = true;
	}

	private void OnBecameInvisible()
	{
		transform.position = _startingPosition;
		birdScript.PipeSpeed += speedIncrease;
		_passed = false;
	}
}
