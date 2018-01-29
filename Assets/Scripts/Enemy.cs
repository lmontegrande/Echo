using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    public enum State { ATTACK, PATROL, FREEZE }

    public GameObject[] wayPoints;
    public State currentState = State.PATROL;
    public EchoCube[] bodyEchoCubes;
    public AudioClip groanAudioClip;
    public AudioClip shacklesAudioClip;
    public float attackTime = 3f;
    public float attackSpeed = 5f;
    public float patrolSpeed = 3f;
    public float delayAttackTime = 1f;
    public bool isSilent = false;

    private NavMeshAgent _navMeshAgent;
    private AudioSource _audioSource;
    private State previousState;
    private GameObject player;
    private int nextWayPointNum = 0;
    private float attackTimer = 0f;
    private bool isDoneAttacking = false;

    private Coroutine currentAttackCoroutine;
    private bool isAttacking = false;

    public void Start()
    {
        foreach(EchoCube bodyEchoCube in bodyEchoCubes)
            bodyEchoCube.OnEcho = OnEcho;
        
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
    }

    public void Update()
    {
        HandleState();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.GetComponent<PlayerManager>().Die();
    }

    public void OnEcho()
    {
        if (currentAttackCoroutine != null)
            StopCoroutine(currentAttackCoroutine);
        currentAttackCoroutine = StartCoroutine(ReactAttack());
    }

    private void HandleState()
    {
        switch (currentState)
        {
            case State.PATROL:
                Patrol();
                break;
            case State.ATTACK:
                if (player == null)
                    player = GameObject.FindGameObjectWithTag("Player");
                Attack();
                break;
            case State.FREEZE:
                Freeze();
                break;
        }
        previousState = currentState;

        if (isDoneAttacking)
        {
            isDoneAttacking = false;
            currentState = State.PATROL;
        }
    }

    private void Patrol()
    {
        // Enter State
        if (previousState != State.PATROL)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(wayPoints[GetNextWayPoint()].transform.position);
            _navMeshAgent.speed = patrolSpeed;
            _audioSource.Stop();
        }

        // In State
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            _navMeshAgent.SetDestination(wayPoints[GetNextWayPoint()].transform.position);
        }
    }

    private void Attack()
    {
        // Enter State
        if (previousState != State.ATTACK)
        {
            attackTimer = 0f;
            _navMeshAgent.isStopped = false;
            _navMeshAgent.speed = attackSpeed;
            _audioSource.Play();
        }

        // In State
        if (attackTimer < attackTime)
        {
            attackTimer += Time.deltaTime;
            _navMeshAgent.SetDestination(player.transform.position);
        }
        else
        {
            isDoneAttacking = true;
        }
    }

    private void Freeze()
    {
        // Enter State
        if (previousState != State.FREEZE)
        {
            _navMeshAgent.isStopped = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (!isSilent)
                AudioManager.instance.GetComponent<AudioSource>().PlayOneShot(groanAudioClip);
        }
    }

    private int GetNextWayPoint()
    {
        int currentWayPointNum = nextWayPointNum;
        nextWayPointNum = nextWayPointNum + 1 >= wayPoints.Length ? 0 : nextWayPointNum + 1;
        return currentWayPointNum;
    }

    private IEnumerator ReactAttack()
    {
        float delayAttackTimer = 0f;
        currentState = State.FREEZE;
        while (delayAttackTimer < delayAttackTime)
        {
            yield return null;
            delayAttackTimer += Time.deltaTime;
        }
        currentState = State.ATTACK;
    }
}
