using System;
using UnityEngine;
using StoneOfAdventure.Core;
using StoneOfAdventure.Movement;

public class PlayerStateController : Unit
{
    #region Variables
    private BaseState idleState;
    [SerializeField] private float movespeed = 5f;
    [SerializeField] private float movespeedInTheAir = 5f;
    public float MovespeedScale = 1f;
    [SerializeField] private float jumpPower = 800f;
    [SerializeField] private float verticalMovespeed = 3f;

    private Mover mover;
    private Climb climb;
    [SerializeField] private string currentState = "";
    #endregion

    private enum State { Idle, MoveHorizontal, MoveVertical, InTheAir, Attack, Skill2 }
    private State playerState = State.Idle;

    private void Start()
    {
        _State = GetComponent<PlayerIdleState>();
        idleState = GetComponent<PlayerIdleState>();
        mover = GetComponent<Mover>();
        climb = GetComponent<Climb>();
    }

    private void Update()
    {
        MoveHorizontal(Input.GetAxisRaw("Horizontal"));
        MoveVertical(Input.GetAxisRaw("Vertical"));
        if (Input.GetAxisRaw("Fire1") != 0f) Attack();
        if (Input.GetAxisRaw("Fire2") != 0f) Skill1();
        if (Input.GetAxisRaw("Fire3") != 0f) Skill2();
        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        currentState = base._State.ToString();
    }

    #region Events
    private void Idle()
    {
        base._State.Idle();
    }

    private void MoveHorizontal(float direction)
    {
        base._State.MoveHorizontal(direction, (base._State == GetComponent<PlayerJumpState>()) ? movespeedInTheAir : (movespeed * MovespeedScale));
    }

    private void MoveVertical(float direction)
    {
        base._State.MoveVertical(direction, verticalMovespeed);
    }

    public override void Attack()
    {
        base._State.Attack();
    }

    private void Skill1()
    {
        base._State.Skill1();
    }

    private void Skill2()
    {
        base._State.Skill2();
    }

    private void Jump()
    {
        base._State.Jump(jumpPower);
    }

    public override void Landed()
    {
        DisableState();
    }

    public override void DisableState()
    {
        base._State = idleState;
    }

    public override void Fell()
    {
        base._State.Fell();
    }
    #endregion

    #region StateTransitions
    private void StateIdle()
    {
        switch (playerState)
        {
            case State.MoveHorizontal:
                mover.Cancel();
                break;
        }
        SetState(State.Idle);
    }

    private void StateMoveHorizontal()
    {
        if (playerState == State.Idle)
        {
            SetState(State.MoveHorizontal);
        }
    }

    private void StateMoveVertical(float direction)
    {
        bool unitCanClimbOnLadder = (direction != 0f && !climb.LadderEnd(direction) && climb.CanClimb);

        switch (playerState)
        {
            case State.Idle:
                if (!unitCanClimbOnLadder) return;
                break;
            case State.MoveHorizontal:
                if (!unitCanClimbOnLadder) return;
                mover.Cancel();
                break;
            case State.InTheAir:
                if (!unitCanClimbOnLadder) return;
                break;
            case State.Attack:
                return;
            case State.Skill2:
                return;
        }
        SetState(State.MoveVertical);
    }

    private void StateInTheAir()
    {
        SetState(State.InTheAir);
    }

    private void StateAttack()
    {
        SetState(State.Attack);
    }

    private void StateSkill2()
    {
        SetState(State.Skill2);
    }
    #endregion

    private void SetState(State value)
    {
        switch (playerState)
        {
            case State.Idle:
                playerState = value;
                break;
            case State.MoveHorizontal:
                playerState = value;
                break;
            case State.MoveVertical:
                playerState = value;
                break;
            case State.InTheAir:
                playerState = value;
                break;
            case State.Attack:
                playerState = value;
                break;
            case State.Skill2:
                playerState = value;
                break;
        }
    }
}
