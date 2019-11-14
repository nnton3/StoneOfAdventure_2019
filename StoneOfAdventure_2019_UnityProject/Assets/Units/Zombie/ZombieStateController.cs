using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using System.Collections;
using System;

public class ZombieStateController : UnitContainsAward
{
    #region Variables
    private EnemyDetector enemyDetector;
    private GameObject currentTarget;
    private ZombieIdleState idleState;
    private BaseState deathState;
    private BaseState inTheAirState;

    private PatrolBehaviour patrolBehaviour;
    private ChaseBehaviour chaseBehaviour;

    [SerializeField] private float movespeed = 3f;
    [SerializeField] private float patrolMovespeed = 1.5f;
    #endregion

    protected override void Start()
    {
        base.Start();

        enemyDetector = GetComponentInChildren<EnemyDetector>();
        patrolBehaviour = GetComponent<PatrolBehaviour>();
        chaseBehaviour = GetComponent<ChaseBehaviour>();

        idleState = GetComponent<ZombieIdleState>();
        deathState = GetComponent<ZombieDeathState>();
        inTheAirState = GetComponent<ZombieInTheAirState>();

        _State = deathState;

        enemyDetector.PlayerDetected.AddListener(UpdateTarget);
        enemyDetector.PlayerLost.AddListener(UpdateTarget);
    }

    private void Update()
    {
        if (_State == deathState) return;
        if (_State == inTheAirState)
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
    }

    public override void Attack() { _State.Attack(); }

    private void MoveHorizontal(float direction, float movespeed) { _State.MoveHorizontal(direction, movespeed); }

    public override void DisableState() { _State = idleState; }

    public override void Dead()
    {
        _State.Dead();
        enemyDetector.PlayerDetected.RemoveAllListeners();
        enemyDetector.PlayerLost.RemoveAllListeners();
        StartCoroutine("DestroyCorrupse");
        CreateReward();
    }

    public override void Fell()
    {
        // State.Fell();
    }

    public override void Landed()
    {
        if (_State == deathState) return;
        DisableState();
    }

    public override void Born()
    {
        
        _State.Born();
    }

    IEnumerator DestroyCorrupse()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public override void ApplyStun(float timeOfStun)
    {
        _State.Stun(timeOfStun);
    }
}
