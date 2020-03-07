using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using System.Collections;
using StoneOfAdventure.Movement;

public class ZombieStateController : UnitContainsAward
{
    #region Variables
    private EnemyDetector enemyDetector;
    private GameObject currentTarget;
    private Mover mover;
    private Fighter fighter;
    private Animator anim;

    private PatrolBehaviour patrolBehaviour;
    private ChaseBehaviour chaseBehaviour;
    #endregion

    protected override void Start()
    {
        base.Start();

        currentState = State.Death;

        enemyDetector = GetComponentInChildren<EnemyDetector>();
        patrolBehaviour = GetComponent<PatrolBehaviour>();
        chaseBehaviour = GetComponent<ChaseBehaviour>();
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();
        anim = GetComponent<Animator>();

        enemyDetector.PlayerDetected.AddListener(UpdateTarget);
        enemyDetector.PlayerLost.AddListener(UpdateTarget);
    }

    private void Update()
    {
        if (currentState == State.Death) return;
        if (currentState == State.InTheAir)
        {
            MoveHorizontal(0f);
            return;
        }
        if (currentTarget)
        {
            chaseBehaviour.UpdateChaseBehaviour();
            MoveHorizontal(chaseBehaviour.CalculateDirection());
        }
        else
        {
            patrolBehaviour.UpdatePatrolBehaviour();
            MoveHorizontal(patrolBehaviour.PatrolDirection);
        }
    }

    public void UpdateTarget()
    {
        currentTarget = enemyDetector.Player;
    }

    #region Events
    public override void MoveHorizontal(float direction)
    {
        switch (currentState)
        {
            case State.Idle:
                if (direction != 0f) StateMoveHorizontal();
                break;
            case State.MoveHorizontal:
                if (direction == 0f) StateIdle();
                else mover.MoveTo(direction);
                break;
            case State.InTheAir:
                mover.MoveInAirTo(direction);
                break;
        }
    }

    public override void Attack()
    {
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.MoveHorizontal:
                mover.CancelMove();
                StateAttack();
                fighter.StartAttack();
                break;
        }
    }

    public override void DisableState() => StateIdle();

    public override void Dead()
    {
        switch (currentState)
        {
            case State.Idle:
                TransformToCorrupse();
                break;
            case State.MoveHorizontal:
                mover.CancelMove();
                TransformToCorrupse();
                break;
            case State.InTheAir:
                mover.CancelMove();
                TransformToCorrupse();
                break;
            case State.Attack:
                fighter.CancelAttack();
                TransformToCorrupse();
                break;
            case State.Stun:
                StopCoroutine("StunTimer");
                TransformToCorrupse();
                break;
        }

        void TransformToCorrupse()
        {
            anim.SetTrigger("dead");
            StateDeath();
            enemyDetector.PlayerDetected.RemoveAllListeners();
            enemyDetector.PlayerLost.RemoveAllListeners();
            StartCoroutine("DestroyCorrupse");
            CreateReward();
        }
    }

    public override void Landed()
    {
        if (currentState == State.Death) return;
        DisableState();
    }

    public override void Born()
    {
        if (currentState == State.Death) DisableState();
    }

    public override void ApplyStun(float timeOfStun)
    {
        if (currentState != State.Death)
        {
            Debug.Log($"go in stun HP = {GetComponent<Health>().HealthPoints}");
            anim.SetTrigger("stun");
            StopCoroutine(StunTimer(timeOfStun));
            StartCoroutine(StunTimer(timeOfStun));
            StateStun();
        }
        else
        {
            Debug.Log("stop stun");
            StopCoroutine(StunTimer(timeOfStun));
        }
    }

    #endregion

    #region StateTransitions
    private void StateIdle()
    {
        if (currentState == State.MoveHorizontal) mover.CancelMove();
        SetState(State.Idle);
    }

    private void StateMoveHorizontal()
    {
        SetState(State.MoveHorizontal);
    }

    private void StateAttack()
    {
        SetState(State.Attack);
    }

    private void StateDeath()
    {
        SetState(State.Death);
    }

    private void StateStun()
    {
        SetState(State.Stun);
    }

    #endregion

    IEnumerator DestroyCorrupse()
    {
        yield return new WaitForSeconds(1f);
        ReturnToPool();
    }

    private void SetState(State value)
    {
        currentState = value;
    }

    protected override void StunEnd()
    {
        anim.SetTrigger("stunEnd");
        DisableState();
    }
}
