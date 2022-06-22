using System;
using UnityEngine;

public class AlarmSound : MonoBehaviour
{
	[SerializeField] private AudioClip starterMusic;
	[SerializeField] private AudioClip alarmMusic;
	[SerializeField] private DetectLight detection;
	private AudioSource _source;

	private void Start()
	{
		_source = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (detection.currentDetection >= 75)
		{
			_source.clip = alarmMusic;
			if (!_source.isPlaying)
				_source.Play();

		}
		else
		{
			_source.clip = starterMusic;
			if (!_source.isPlaying)
			{
				_source.Play();
			}
		}
	}
}
