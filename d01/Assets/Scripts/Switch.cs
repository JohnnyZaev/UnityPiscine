using UnityEngine;

public class Switch : MonoBehaviour
{
	private bool IsSwitched { get; set; }
	[SerializeField] private Transform doorTransform;
	[SerializeField] private BoxCollider2D doorCollider2D;
	private Vector3 _doorStarterPosition;
	private float _endPosition;
	private bool _inTriggerRadius;
	private bool _isActive;

	private void Awake()
	{
		var position = doorTransform.position;
		_doorStarterPosition = position;
		_endPosition = doorCollider2D.bounds.size.y + position.y;
		IsSwitched = false;
	}

	private void Update()
	{
		if (_inTriggerRadius)
		{
			if (Input.GetKeyDown(KeyCode.F) && _isActive)
				IsSwitched = !IsSwitched;
		}
		if (IsSwitched)
		{
			if (doorTransform.position.y <= _endPosition)
			{
				doorTransform.position += Vector3.up * Time.deltaTime;
			}
		}
		else
		{
			if (doorTransform.position.y >= _doorStarterPosition.y)
			{
				doorTransform.position += Vector3.down * Time.deltaTime;
			}
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.GetComponent<playerScript_ex01>().IsActive == false)
		{
			_isActive = false;
			return;
		}

		_isActive = true;

		_inTriggerRadius = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		_inTriggerRadius = false;
	}
}
