using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private KeyCode upKey;
	[SerializeField] private KeyCode downKey;
	[SerializeField] private Transform[] collisions;
	[SerializeField] private float movementSpeed;
	private const int TopCollision = 0;
	private const int DownCollision = 1;

	private void Update()
	{
		if (Input.GetKey(upKey) && transform.position.y + 1f < collisions[TopCollision].position.y)
			transform.Translate(Vector3.up * (Time.deltaTime * movementSpeed));
		if (Input.GetKey(downKey) && transform.position.y - 1f > collisions[DownCollision].position.y)
			transform.Translate(Vector3.down * (Time.deltaTime * movementSpeed));
	}
}
