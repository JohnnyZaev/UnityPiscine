using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool isFly;

    public float speed;
    private float _damage;
    
    private Vector3 _dir;
    private Rigidbody _rb;
    private bool _isDamage;
    
    public void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        if (isFly)
        {
            _rb.AddForce(_dir * (Time.deltaTime * speed), ForceMode.Impulse);
        }
    }

    private void LateUpdate()
    {
        if (!_isDamage)
        {
            explosionDamage();
        }
    }

    public void fly(GameObject go)
    {
        _dir = -go.transform.up;
        gameObject.tag = go.tag;
        isFly = true;
        _isDamage = false;
    }

    public void setDamage(float damage)
    {
        _damage = damage;
    }

    private void explosionDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, 0.4f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag.Equals("EnemyObject"))
            {
                hitColliders[i].SendMessage("hit", _damage);
                _isDamage = true;
            }
            i++;
        }
    }
}
