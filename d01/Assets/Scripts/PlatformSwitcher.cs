using UnityEngine;

public class PlatformSwitcher : MonoBehaviour
{
	[SerializeField] private GameObject platform;
	private SpriteRenderer _platformSpriteRenderer;
	private bool _inTriggerRadius;
	private int _thomas;
	private int _john;
	private int _clair;
	private int _currentLayer;
	private Color _color;
	private bool _isActive;

	private void Awake()
	{
		_platformSpriteRenderer = platform.GetComponent<SpriteRenderer>();
		_thomas = LayerMask.NameToLayer("RedGround");
		_john = LayerMask.NameToLayer("YellowGround");
		_clair = LayerMask.NameToLayer("BlueGround");
		_currentLayer = _john;
	}

	private void Update()
	{
		if (!_inTriggerRadius) return;
		if (!Input.GetKeyDown(KeyCode.F) || _currentLayer == platform.layer || !_isActive) return;
		platform.layer = _currentLayer;
		_platformSpriteRenderer.color = _color;
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.GetComponent<playerScript_ex01>().IsActive == false)
		{
			_isActive = false;
			return;
		}

		_isActive = true;
		switch (other.name)
		{
			case "Thomas":
				_currentLayer = _thomas;
				_color = Color.red;
				break;
			case "John":
				_currentLayer = _john;
				_color = Color.yellow;
				break;
			case "Clair":
				_currentLayer = _clair;
				_color = Color.blue;
				break;
		}

		_inTriggerRadius = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		_inTriggerRadius = false;
	}
}
