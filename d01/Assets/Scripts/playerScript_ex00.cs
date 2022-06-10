using UnityEngine;

public class playerScript_ex00 : MonoBehaviour
{
	[SerializeField] private float playerSpeed;
	[SerializeField] private float playerJumpForce;
	[SerializeField] private LayerMask platformLayerMask;
	[SerializeField] private LayerMask playerLayerMask;
	public bool IsActive { get; set; }
	private Rigidbody2D _playerRb;
	private BoxCollider2D _playerBoxCollider2D;
	private float _horizontalMovement;
	private bool _jumpButton;
	private bool _jumpButtonReleased;

	private void Awake()
	{
		_playerRb = GetComponent<Rigidbody2D>();
		_playerBoxCollider2D = GetComponent<BoxCollider2D>();
		_playerRb.bodyType = RigidbodyType2D.Static;
	}

	private void FixedUpdate()
	{
		if (!IsActive)
		{
			if (_playerRb.bodyType != RigidbodyType2D.Dynamic) return;
			_playerRb.bodyType = RigidbodyType2D.Static;

			return;
		}

		if (_playerRb.bodyType == RigidbodyType2D.Static)
		{
			_playerRb.bodyType = RigidbodyType2D.Dynamic;
		}

		_playerRb.velocity = new Vector2(_horizontalMovement * Time.fixedDeltaTime * playerSpeed, _playerRb.velocity.y);
	}

	private void Update()
	{
		if (!IsActive)
			return;
		_horizontalMovement = Input.GetAxisRaw("Horizontal");
		_jumpButton = Input.GetKeyDown(KeyCode.Space);
		_jumpButtonReleased = Input.GetKeyUp(KeyCode.Space);
		
		if (_jumpButton && IsGrounded())
			_playerRb.velocity = new Vector2(_playerRb.velocity.x, playerJumpForce);
		if (_jumpButtonReleased && _playerRb.velocity.y > 0f)
			_playerRb.velocity = new Vector2(_playerRb.velocity.x, 0);

	}
	
	private bool IsGrounded()
	{
		const float extraHeightTest = 0.1f;
		var bounds = _playerBoxCollider2D.bounds;
		var raycastHit = Physics2D.BoxCast(bounds.center , bounds.size , 0f, Vector2.down, extraHeightTest, 
		platformLayerMask);
		var raycastHit2 = Physics2D.BoxCastAll(bounds.center, bounds.size, 0f, Vector2.down, extraHeightTest, 
		playerLayerMask);
		return raycastHit.collider || raycastHit2.Length > 1;
	}
	
}
