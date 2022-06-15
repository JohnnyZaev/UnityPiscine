using UnityEngine;

public class TownHall : MonoBehaviour
{
	[SerializeField] private GameObject unitPrefab;
	[SerializeField] private Transform spawnPosition;
	private float _spawnTime = 10f;
	private float _currentSpawnTime;

	private void Awake()
	{
		_currentSpawnTime = _spawnTime;
	}

	private void Update()
	{
		if (_currentSpawnTime < _spawnTime)
		{
			_currentSpawnTime += Time.deltaTime;
			return;
		}

		Instantiate(unitPrefab, spawnPosition.position, Quaternion.identity);
		_currentSpawnTime = 0f;
	}

	public void IncreaseSpawnTime()
	{
		_spawnTime += 2.5f;
	}

	public void PrintAllienceWin()
	{
		Debug.Log("The Human Team wins.");
	}

	public void PrintHordeTeamWin()
	{
		Debug.Log("The Orc Team wins.");
	}
}
