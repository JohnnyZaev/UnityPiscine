using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
	[SerializeField] private Transform[] unitsTransforms;
	[SerializeField] private playerScript_ex00[] playerScripts;
	private Vector3 _cameraOffset;
	private Transform _cameraTransform;
	private const int Thomas = 0;
	private const int John = 1;
	private const int Clair = 2;
	private int _currentPlayer;

	private void Awake()
	{
		_cameraTransform = transform;
		_cameraOffset = _cameraTransform.position;
		_cameraTransform.position = unitsTransforms[John].position;
		_currentPlayer = Thomas;
		playerScripts[_currentPlayer].IsActive = true;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		if (Input.GetKeyDown(KeyCode.Alpha1) && _currentPlayer != Thomas)
		{
			ChangePlayer(Thomas);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2) && _currentPlayer != John)
		{
			ChangePlayer(John);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3) && _currentPlayer != Clair)
		{
			ChangePlayer(Clair);
		}

		transform.position = unitsTransforms[_currentPlayer].position + _cameraOffset;
	}

	private void ChangePlayer(int player)
	{
		playerScripts[_currentPlayer].IsActive = false;
		_currentPlayer = player;
		playerScripts[_currentPlayer].IsActive = true;
	}
}
