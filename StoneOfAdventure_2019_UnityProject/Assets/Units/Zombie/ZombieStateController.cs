using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using System.Collections;
using System;

public class ZombieStateController : Unit
{
    #region Variables
    private EnemyDetector enemyDetector;
    private GameObject currentTarget;
    private ZombieIdleState idleState;
    private BaseState deathState;
    private BaseState inTheAirState;

    private PatrolBehaviour patrolBehaviour;
    private ChaseBehaviour chaseBehaviour;
    private GameObject soulShard;

    [SerializeField] private float movespeed = 3f;
    [SerializeField] private float patrolMovespeed = 1.5f;
    [SerializeField] private int reward = 3;
    #endregion

    private void Start()
    {
        enemyDetector = GetComponentInChildren<EnemyDetector>();
        patrolBehaviour = GetComponent<PatrolBehaviour>();
        chaseBehaviour = GetComponent<ChaseBehaviour>();
        soulShard = Resources.Load<GameObject>("Soul_shard");

        idleState = GetComponent<ZombieIdleState>();
        deathState = GetComponent<ZombieDeathState>();
        inTheAirState = GetComponent<ZombieInTheAirState>();

        State = deathState;
        
        enemyDetector.PlayerDetected.AddListener(() => UpdateTarget());
        enemyDetector.PlayerLost.AddListener(() => UpdateTarget());
    }

    private void Update()
    {
        if (State == deathState) return;
        if (State == inTheAirState)
        {
            MoveHorizontal(0f, patrolMovespeed);
            return;
        }
        if (currentTarget)
        {
            chaseBehaviour.UpdateChaseBegaviour();
            MoveHorizontal(chaseBehaviour.CalculateDirection(), movespeed);
        }
        else
        {
            patrolBehaviour.UpdatePatrolBehaviour();
            MoveHorizontal(patrolBehaviour.PatrolDirection, patrolMovespeed);
        }
    }

    public void UpdateTarget()
    {
        currentTarget = enemyDetector.Player;
        if (currentTarget == null) DisableState();
    }

    public override void Attack() { State.Attack(); }

    private void MoveHorizontal(float direction, float movespeed) { State.MoveHorizontal(direction, movespeed); }

    public override void DisableState() { State = idleState; }

    public override void Dead()
    {
        State.Dead();
        enemyDetector.PlayerDetected.RemoveAllListeners();
        enemyDetector.PlayerLost.RemoveAllListeners();
        StartCoroutine("DestroyCorrupse");
        CreateReward();
    }

    private void CreateReward()
    {
        for (int i = 0; i < reward; i++)
        {
            Debug.Log("create shard");
            Instantiate(soulShard, transform.position, Quaternion.identity);
        }
    }

    public override void Fell()
    {
        State.Fell();
    }

    public override void Landed()
    {
        if (State == deathState) return;
        DisableState();
    }

    public override void Born()
    {
        State.Born();
    }

    IEnumerator DestroyCorrupse()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public override void ApplyStun(float timeOfStun)
    {
        State.Stun(timeOfStun);
    }
}
