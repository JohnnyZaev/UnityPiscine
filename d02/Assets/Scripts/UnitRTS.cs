using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class UnitRTS : MonoBehaviour
{
	[SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float maxAttackDistance;
    [SerializeField] private float attackDelay;
    
    [SerializeField] private UnityEvent onTargetPositionChanged;
    [SerializeField] private UnityEvent onSelected;
    [SerializeField] private UnityEvent onAttack;
    
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private HealthSystem _target;
    private Vector3 _targetPosition;
    
    private float _currentAttackDelay;
    private static readonly int Y = Animator.StringToHash("Y");
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private SpriteRenderer _spriteRenderer;
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");

    private void Awake()
    {
	    _spriteRenderer = GetComponent<SpriteRenderer>();
	    _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _targetPosition = transform.position;
    }

    private void Update()
    {
        _currentAttackDelay -= Time.deltaTime;
        
        if (_target
            && (_target.transform.position - transform.position).sqrMagnitude <= maxAttackDistance * maxAttackDistance)
        {
            _animator.SetBool(IsAttacking, true);
            if (!(_currentAttackDelay <= 0)) return;
            _target.GetComponent<HealthSystem>().TakeDamage(damage);
            onAttack?.Invoke();
            _currentAttackDelay = attackDelay;
            return;
        }
        
        _animator.SetBool(IsAttacking, false);
        
        if (_target)
        {
            _targetPosition = _target.transform.position;
        }
        
        var direction = (_targetPosition - transform.position).normalized;
        _animator.SetFloat(X, direction.x);
        _animator.SetFloat(Y, direction.y);
        _spriteRenderer.flipX = direction.x < 0f;

        MoveTo(_targetPosition);
    }

    private void MoveTo(Vector3 position)
    {
        if ((position - transform.position).sqrMagnitude > 0.1f)
        {
            var newPosition = Vector3.MoveTowards(transform.position, position, Time.deltaTime * speed);
            _animator.SetBool(IsMoving, true);
            _rigidbody.MovePosition(newPosition);
        }
        else
        {
	        _animator.SetFloat(X, 0);
	        _animator.SetFloat(Y, 0);
	        _animator.SetBool(IsMoving, false);
        }
    }

    public void UpdateTarget(HealthSystem newTarget)
    {
        _target = newTarget;
    }

    public void UpdateTargetPosition(Vector3 newTargetPosition)
    {
        newTargetPosition.z = transform.position.z;
        _target = null;
        _targetPosition = newTargetPosition;
        onTargetPositionChanged?.Invoke();
    }

    public void Select()
    {
        onSelected?.Invoke();   
    }
}
