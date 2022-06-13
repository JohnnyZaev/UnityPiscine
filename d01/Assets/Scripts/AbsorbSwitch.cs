using UnityEngine;

public class AbsorbSwitch : MonoBehaviour
{
	public bool IsSwitched { get; private set; }
	[SerializeField] private Transform[] doorTransform;
	[SerializeField] private BoxCollider2D[] doorCollider2D;
	private readonly Vector3[] _doorStarterPosition = new Vector3[3];
	private readonly float[] _endPosition = new float[3];
	private bool _inTriggerRadius;
	private const int Thomas = 0;
	private const int John = 1;
	private const int Clair = 2;
	private int _currentColour;

	private void Awake()
	{
		for (var i = 0; i < doorTransform.Length; i++)
		{
			var position = doorTransform[i].position;
			_doorStarterPosition[i] = position;
			_endPosition[i] = doorCollider2D[i].bounds.size.y + position.y;
		}
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
			if (doorTransform[_currentColour].position.y <= _endPosition[_currentColour])
			{
				doorTransform[_currentColour].position += Vector3.up * Time.deltaTime;
			}
		}
		else
		{
			if (doorTransform[_currentColour].position.y >= _doorStarterPosition[_currentColour].y)
			{
				doorTransform[_currentColour].position += Vector3.down * Time.deltaTime;
			}
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.name == "Thomas")
			_currentColour = Thomas;
		if (other.name == "John")
			_currentColour = John;
		if (other.name == "Clair")
			_currentColour = Clair;
		_inTriggerRadius = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		_inTriggerRadius = false;
	}
}