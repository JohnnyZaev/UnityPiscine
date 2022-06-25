using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerBoss : AliveObject
{

    public bool isPlayer;
    public bool isAttack;
    
    public GameObject playerObject;
    public GameObject player;
	public PlayerController playerController;
    public GameObject loot;

    
    private NavMeshAgent agent;
    private Animator _animator;
    private Vector3 checkDeadPoint;
    private Vector3 checkPoint;

    [HideInInspector]public GameObject _parent;
    [SerializeField] private string _type;
    private static readonly int Dead = Animator.StringToHash("Dead");
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Attack = Animator.StringToHash("attack");


    /*
     * Unity api methods
     */
    private void Start()
    {
		player = GameObject.Find("Maya");
        GetComponent<CapsuleCollider>();
        _animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        isSpawn = true;
        updateState();
    }

    private void Update()
    {
        if (hp > 0)
        {
            AliveLogic();
            passiveSkills();
			if (Vector3.Distance(transform.position, player.transform.position) < 0.7)
			{
				targetOnPlayer(player);
			}
        }
    }

    /*
     * Public action
     */
    public void attack()
    {
        if (75 + agility - playerController.agility > 60)
        {
            playerController.hit(Random.Range(minDamage, maxDamage));
            if (playerController.hp < 0)
            {
                isPlayer = false;
            }
        }
    }
    
    public void hit(float damageTmp)
    {
        if (!isDead)
        {
            hp -= System.Convert.ToInt32(damageTmp * (1 - armor / 200));
            if (hp <= 0)
            {
                hp = 0;
                isExp = true;
                _animator.SetBool(Dead, true);
                isDead = true;
                SpawnLoot();
            }
        }
        if (!isPlayer)
        {
            FindPlayer();
        }
    }

    private void FindPlayer()
    {
        targetOnPlayer(GameObject.Find("Maya"));
    }
    
    public void upgradeStat()
    {
        strengh += (strengh * 0.08f);
        constitution += (constitution * 0.15f);
        agility += (agility * 0.05f);
        exp += (exp * 0.15f);
        updateHp();
        updateDamage();
    }

    public override string GetTypeObject()
    {
        return _type;
    }
    
    private void AliveLogic()
    {
        if (!isSpawn)
        {
            SpawnMove();
        }
        else if (isPlayer)
        {
            agent.SetDestination(playerObject.transform.position);
            _animator.SetBool(Run, true);
            if (!isAttack)
            {
                _animator.SetBool(Run, true);
            }

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                _animator.SetBool(Run, false);
                _animator.SetBool(Attack, true);
                isAttack = true;
                RotateForAttack();
            }

            if (agent.remainingDistance >= agent.stoppingDistance)
            {
                isAttack = false;
                _animator.SetBool(Attack, false);
            }
        }
    }

    private void SpawnMove()
    {
            isSpawn = true;
            agent.Warp(transform.position);
    }
    
    private void SpawnLoot()
    {
        if (Random.Range(0, 15) > 10)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
    }
    
    private void RotateForAttack()
    {
        Vector3 difference = playerObject.gameObject.transform.position - transform.position; 
        difference.Normalize();
        float rotationY = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;
        agent.transform.rotation = Quaternion.Euler(0f, -rotationY + 90, 0);
        
    }
    
    private void targetOnPlayer(GameObject targetPlayer)
    {
        if (targetPlayer.GetComponent<PlayerController>().hp > 0)
        {
            playerObject = targetPlayer.gameObject;
            playerController = targetPlayer.gameObject.GetComponent<PlayerController>();
            isPlayer = true;
        }
    }
}
