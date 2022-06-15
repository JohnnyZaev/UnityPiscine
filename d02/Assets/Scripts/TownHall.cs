using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHall : MonoBehaviour
{
	[SerializeField] private GameObject unitPrefab;
	[SerializeField] private Transform spawnPosition;
	private const float _spawnTime = 10f;
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
}
