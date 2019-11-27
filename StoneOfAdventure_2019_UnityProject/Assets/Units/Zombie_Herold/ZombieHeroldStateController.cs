using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using System.Collections;
using UnityEngine.Experimental.VFX;
using StoneOfAdventure.Movement;

public class ZombieHeroldStateController : UnitContainsAward
{
    #region Variables
    private Flip flip;
    private EnemyDetector enemyDetector;
    private object target;
    private GameObject player;
    private PatrolBehaviour patrolBehaviour;
    private Mover mover;
    private Fighter fighter;
    private Animator anim;
    private Stunned stunned;

    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float movespeed = 3f;
    #endregion

    protected override void Start()
    {
        base.Start();

        flip = GetComponent<Flip>();
        patrolBehaviour = GetComponent<PatrolBehaviour>();
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();
        anim = GetComponent<Animator>();
        stunned = GetComponent<Stunned>();

        currentState = State.Death;
    }

    private void Update()
    {
        patrolBehaviour.UpdatePatrolBehaviour();

        MoveHorizontal(patrolBehaviour.PatrolDirection, movespeed);
    }

    #region Events
    private void MoveHorizontal(float direction, float movespeed)
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
                TransitionToCorrupse();
                break;
            case State.MoveHorizontal:
                mover.CancelMove();
                TransitionToCorrupse();
                break;
            case State.InTheAir:
                mover.CancelMove();
                TransitionToCorrupse();
                break;
            case State.Attack:
                fighter.CancelAttack();
                TransitionToCorrupse();
                break;
            case State.Stun:
                TransitionToCorrupse();
                break;
        }

        void TransitionToCorrupse()
        {
            anim.SetTrigger("dead");
            StateDeath();
            GetComponentInChildren<VisualEffect>().Stop();
            GetComponentInChildren<BuffArea>().RemoveAllBuffs();
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
        switch (currentState)
        {
            case State.Idle:
                stunned.ApplyStun(timeOfStun);
                StateStun();
                break;
            case State.MoveHorizontal:
                mover.CancelMove();
                stunned.ApplyStun(timeOfStun);
                StateStun();
                break;
            case State.Attack:
                fighter.CancelAttack();
                stunned.ApplyStun(timeOfStun);
                StateStun();
                break;
        }
    }
    #endregion

    IEnumerator DestroyCorrupse()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

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

    private void SetState(State value)
    {
        currentState = value;
    }
}
