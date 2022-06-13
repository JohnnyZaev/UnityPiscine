using UnityEngine;

public class Switch : MonoBehaviour
{
	public bool IsSwitched { get; private set; }
	[SerializeField] private Transform doorTransform;
	[SerializeField] private BoxCollider2D doorCollider2D;
	private Vector3 _doorStarterPosition;
	private float _endPosition;
	private bool _inTriggerRadius;

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
			if (Input.GetKeyDown(KeyCode.F))
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
		_inTriggerRadius = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		_inTriggerRadius = false;
	}
}
