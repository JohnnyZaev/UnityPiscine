using UnityEngine;

public class Ball : MonoBehaviour
{
	[SerializeField] private GameObject club;
	[SerializeField] private float flySeconds;
	[SerializeField] private Transform topCollision;
	[SerializeField] private Transform bottomCollision;
	[SerializeField] private Transform hole;
	[SerializeField] private float holeOffset;
	private bool _isPunched;
	private float _punchPower;
	private float _currentSeconds;
	private float _score = -15;

	private void Update()
	{
		if (!_isPunched)
			return;
		_currentSeconds += Time.deltaTime;
		if (transform.position.y >= topCollision.position.y || transform.position.y <= bottomCollision.transform
			    .position.y)
			_punchPower *= -1;
		if (_currentSeconds <= flySeconds)
			transform.Translate(Vector3.up * (_punchPower * Time.deltaTime));
		else
		{
			_currentSeconds = 0;
			_isPunched = false;
			club.SetActive(true);
			Debug.Log("Score: " + _score);
			if (transform.position.y >= hole.transform.position.y - holeOffset && transform.position.y <= hole.position.y 
			+ holeOffset)
			{
				Destroy(gameObject);
				Destroy(club);
				Debug.Log(_score < 0f ? "You win!" : "You lose!");
			}
			_score += 5;
		}
	}

	public void Punch(float power)
	{
		_isPunched = true;
		_punchPower = power;
		club.SetActive(false);
	}
}
