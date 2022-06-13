using UnityEngine;

public class Lift : MonoBehaviour
{
	[SerializeField] float offsetUp, offsetDown, speed = 1;
	[SerializeField] bool hasReachedUp, hasReachedDown;
	Vector3 _startPosition = Vector3.zero;
 
	void Awake()
	{
		_startPosition = transform.position;
	}

	private void FixedUpdate()
	{
		if (!hasReachedUp)
		{
			if (transform.position.y <= _startPosition.y + offsetUp)
			{
				Move(offsetUp);        
			}
			else
			{
				hasReachedUp = true;
				hasReachedDown = false;
			}
		}
		else if (!hasReachedDown)
		{
			if (transform.position.y > _startPosition.y + offsetDown)
			{
				Move(-_startPosition.y);
			}
			else
			{
				hasReachedUp = false;
				hasReachedDown = true;
			}
		}
	}
 
	void Move(float offset)
	{
		var position = transform.position;
		position = Vector3.MoveTowards(position,
			new Vector3(_startPosition.x,
				position.y + offset,
				position.z),
			speed * Time.deltaTime);
		transform.position = position;
	}
}
