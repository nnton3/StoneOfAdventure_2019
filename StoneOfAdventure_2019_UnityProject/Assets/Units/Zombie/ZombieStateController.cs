using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using System.Collections;

public class ZombieStateController : Unit
{
    private EnemyDetector enemyDetector;
    private GameObject currentTarget;
    private ZombieIdleState idleState;
    private BaseState deathState;
    private BaseState inTheAirState;

    private PatrolBehaviour patrolBehaviour;
    private ChaseBehaviour chaseBehaviour;

    [SerializeField] private float movespeed = 3f;
    [SerializeField] private float patrolMovespeed = 1.5f;

    private void Start()
    {
        enemyDetector = GetComponentInChildren<EnemyDetector>();
        patrolBehaviour = GetComponent<PatrolBehaviour>();
        chaseBehaviour = GetComponent<ChaseBehaviour>();

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

    public override void DisableState() { Debug.Log("work"); State = idleState; }

    public override void Dead()
    {
        State.Dead();
        enemyDetector.PlayerDetected.RemoveAllListeners();
        enemyDetector.PlayerLost.RemoveAllListeners();
        StartCoroutine("DestroyCorrupse");
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
