using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
	[SerializeField] private GameObject[] cubePrefabs;
	[SerializeField] private Transform[] starterPoints;
	[SerializeField] private float minDelay;
	[SerializeField] private float maxDelay;
	private float _spawnTime;
	private GameObject _currentObject;
	private GameObject _lastObject;

	private void Awake()
	{
		_spawnTime = Time.timeSinceLevelLoad;
	}

	private void Update()
	{
		if (!(Time.timeSinceLevelLoad >= _spawnTime)) return;
		_currentObject = cubePrefabs[Random.Range(0, 3)];
		if (_currentObject == _lastObject) return;
		Instantiate(_currentObject, starterPoints[Random.Range(0, 3)].position, Quaternion.identity);
		_lastObject = _currentObject;
		_spawnTime = Time.timeSinceLevelLoad + Random.Range(minDelay, maxDelay);
	}
}
