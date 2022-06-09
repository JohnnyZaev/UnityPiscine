using UnityEngine;

public class Club : MonoBehaviour
{
	[SerializeField] private GameObject ball;
	[SerializeField] private Ball ballScript;
	[SerializeField] private Transform hole;
	[SerializeField] private float maxPower;
	[SerializeField] private float maxPowerTime;
	[SerializeField] private float offset;
	private bool _isActive;
	private float _currentPower;
	private Vector3 _clubPositionPosition;
	private bool _reversePower;

	private void Awake()
	{
		_currentPower = 0;
		_isActive = true;
	}

	private void Update()
	{
		if (!_isActive)
			return;
		if (hole.position.y > transform.position.y)
		{
			var transform1 = transform;
			var transformRotation = transform1.rotation;
			transformRotation.z = 0;
			transform1.rotation = transformRotation;
			_reversePower = false;
		}
		else
		{
			var transform1 = transform;
			var transformRotation = transform1.rotation;
			transformRotation.z = 180;
			transform1.rotation = transformRotation;
			_reversePower = true;
		}

		if (Input.GetKey(KeyCode.Space))
		{
			_currentPower += maxPower * Time.deltaTime / maxPowerTime;
			if (_reversePower)
				transform.position = ball.transform.position + Vector3.up * Mathf.Clamp(offset - -_currentPower, 
				offset, 1f);
			else
			{
				transform.position = ball.transform.position + Vector3.up * Mathf.Clamp(offset - _currentPower, 
					-1f, offset);
			}
			if (_currentPower >= maxPower)
				_currentPower = maxPower;
		}
		else
		{
			transform.position = ball.transform.position + Vector3.up * offset;
			if (!(_currentPower > 0)) return;
			if (_reversePower)
				_currentPower *= -1;
			ballScript.Punch(_currentPower);
			_currentPower = 0;
		}
			
	}
}
