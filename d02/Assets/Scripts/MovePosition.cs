using UnityEngine;

public class MovePosition : MonoBehaviour
{
	private Vector3 _movePosition;
	private MoveVelocity _moveVelocity;

	private void Awake()
	{
		_moveVelocity = GetComponent<MoveVelocity>();
		_movePosition = transform.position;
	}

	public void SetMovePosition(Vector3 movePosition)
	{
		_movePosition = movePosition;
	}

	private void Update()
	{
		var moveDir = (_movePosition - transform.position).normalized;
		_moveVelocity.SetVelocity(moveDir);
	}
}
