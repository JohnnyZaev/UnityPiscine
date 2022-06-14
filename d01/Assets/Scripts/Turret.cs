using UnityEngine;

public class Turret : MonoBehaviour
{
	[SerializeField] private GameObject bullet;
	[SerializeField] private float fireRate;
	[SerializeField] private string[] playerTag;
	[SerializeField] private float bulletSpeed;
	[SerializeField] [Range(0, 3)] private int target;
	private GameObject _player;
	private bool _isFire;
	private float _currentFireRate;
	private string _currentTag;
	private int _thomas;
	private int _john;
	private int _clair;
	private int _currentLayer;
	private Color _color;
	private bool _isActive = true;

	private void Awake()
	{
		_currentFireRate = fireRate;
		_thomas = LayerMask.NameToLayer("RedGround");
		_john = LayerMask.NameToLayer("YellowGround");
		_clair = LayerMask.NameToLayer("BlueGround");
		switch (target)
		{
			case 0:
				return;
			case 1:
				_currentLayer = _thomas;
				_color = Color.red;
				break;
			case 2:
				_currentLayer = _john;
				_color = Color.yellow;
				break;
			case 3:
				_currentLayer = _clair;
				_color = Color.blue;
				break;
		}
	}

	private void Update()
	{
		if (_isActive)
			_currentFireRate += Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		foreach (var pTag in playerTag)
		{
			if (col.CompareTag(pTag))
			{
				_currentTag = pTag;
				break;
			}

			_currentTag = null;
		}

		if (_currentTag == null) return;
		_player = col.gameObject;
		Shoot();
	}
	
	private void OnTriggerStay2D(Collider2D col)
	{
		foreach (var pTag in playerTag)
		{
			if (col.CompareTag(pTag))
			{
				_currentTag = pTag;
				break;
			}

			_currentTag = null;
		}

		if (_currentTag == null) return;
		_player = col.gameObject;
		_isActive = _player.GetComponent<playerScript_ex01>().IsActive;
		if (!(_currentFireRate >= fireRate) || !_isActive) return;
		Shoot();
		_currentFireRate = 0;
	}

	private void Shoot()
	{
		GameObject currentBullet = Instantiate(bullet, transform.position, Quaternion.identity);
		if (target == 0)
			currentBullet.GetComponent<Bullet>().Construct(bulletSpeed, _player.transform.position - currentBullet
		.transform.position);
		else
		{
			currentBullet.GetComponent<Bullet>().Construct(bulletSpeed, _player.transform.position - currentBullet
				.transform.position, _currentLayer, _color);
		}
	}
}
