using System;
using UnityEngine;

public class Switch : MonoBehaviour
{
	public bool IsSwitched { get; private set; }
	[SerializeField] private Transform doorTransform;
	[SerializeField] private BoxCollider2D doorCollider2D;
	private Vector3 _doorStarterPosition;
	private float _endPosition;
	private bool inTriggerRadius;

	private void Awake()
	{
		_doorStarterPosition = doorTransform.position;
		_endPosition = doorCollider2D.bounds.size.y + doorTransform.position.y;
		IsSwitched = false;
	}

	private void Update()
	{
		if (inTriggerRadius)
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
		inTriggerRadius = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		inTriggerRadius = false;
	}
}
