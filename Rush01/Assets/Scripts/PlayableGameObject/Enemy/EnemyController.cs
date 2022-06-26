﻿using UnityEngine;
using UnityEngine.AI;

public class EnemyController : AliveObject
{

    public bool isPlayer;
    public bool isAttack;
    public GameObject[] weapons;
    public GameObject playerObject;
    public GameObject player;
	public PlayerController playerController;
    public GameObject loot;
    
    private NavMeshAgent agent;
    private Animator _animator;
    private CapsuleCollider capsuleCollider;
    private Vector3 checkDeadPoint;
    private Vector3 checkPoint;

    [HideInInspector]public GameObject _parent;
    [SerializeField] private string _type;
    [SerializeField] private float _speedDownDead;
    [SerializeField] private float _speedUpSpawn;
    private GameObject _player;
    private EnemySpawner _enemySpawner;
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Attack = Animator.StringToHash("attack");

    private void Start()
    {
	    _enemySpawner = _parent.GetComponent<EnemySpawner>();
	    _player = GameObject.Find("Maya");
	    player = GameObject.Find("Maya");
        capsuleCollider = GetComponent<CapsuleCollider>();
        _animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        checkPoint = _parent.GetComponent<EnemySpawner>().enemyCheckPointUp.transform.position;
        updateState();
    }

    private void Update()
    {
        if (hp > 0)
        {
            aliveLogic();
            passiveSkills();
            if (Vector3.Distance(transform.position, _player.transform.position) < 0.7)
			{
				targetOnPlayer(_player);
			}
        }
        else if (hp <= 0)
        {
            deadLogic();
        }
    }
    
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
    
    public override void hit(float damageTmp)
    {
        if (!isDead)
        {
            hp -= System.Convert.ToInt32(damageTmp * (1 - armor / 200));
            if (hp <= 0)
            {
                hp = 0;
                isExp = true;
                _animator.SetBool("Dead", true);
                isDead = true;
                spawnLoot();
            }
        }
        if (!isPlayer)
        {
            findPlayer();
        }
    }

    private void findPlayer()
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
    
    private void deadLogic()
    {
        transform.position = Vector3.MoveTowards(transform.position, checkDeadPoint, _speedDownDead * Time.deltaTime);
        if (Vector3.Distance(transform.position, checkDeadPoint) < 0.0001f)
        {
            _enemySpawner.isEnemy = false;
            Destroy(gameObject);
                
        }
    }

    private void aliveLogic()
    {
        if (!isSpawn)
        {
            spawnMove();
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
                rotateForAttack();
            }

            if (agent.remainingDistance >= agent.stoppingDistance)
            {
                isAttack = false;
                _animator.SetBool(Attack, false);
            }
        }
    }

    private void spawnMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, checkPoint, _speedUpSpawn * Time.deltaTime);
        if (Vector3.Distance(transform.position, checkPoint) < 0.0001f)
        {
            isSpawn = true;
            agent.Warp(transform.position);
        }
    }
    
    private void spawnLoot()
    {
        if (Random.Range(0, 15) > 10)
        {
            Instantiate(loot, transform.position, Quaternion.identity);
        }
        else if (Random.Range(0, 15) > 5)
        {
            GameObject weap = Instantiate(weapons[Random.Range(0, 3)], transform.position, Quaternion.identity);
            weap.transform.localScale *= 7;
            if ((int) weap.GetComponent<WeaponStats>().weaponType == 1)
            {
                Vector3 euler = weap.transform.eulerAngles;
                euler.x += 90;
                weap.transform.eulerAngles = euler;
            }

            weap.GetComponent<WeaponStats>().damage = Random.Range(10 * level, 30 * level);
            weap.GetComponent<WeaponStats>().attackSpeed = Random.Range(0.3f * level, 0.8f * level);
        }
    }
    
    private void rotateForAttack()
    {
        Vector3 difference = playerObject.gameObject.transform.position - transform.position; 
        difference.Normalize();
        float rotation_y = Mathf.Atan2(difference.z, difference.x) * Mathf.Rad2Deg;
        agent.transform.rotation = Quaternion.Euler(0f, -rotation_y + 90, 0);
        
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

    private void deadEndAnimation()
    {
        isDead = true;
        isAttack = false;
        isPlayer = false;
        isSpawn = false;
        agent.enabled = false;
        capsuleCollider.enabled = false;
        var position = transform.position;
        checkDeadPoint = new Vector3(position.x, position.y - 0.15f, position.z);
    }
}

