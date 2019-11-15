using System;
using UnityEngine;
using StoneOfAdventure.Core;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Combat;

public class PlayerStateController : Unit
{
    #region Variables
    private BaseState idleState;
    [SerializeField] private float movespeed = 5f;
    [SerializeField] private float movespeedInTheAir = 5f;
    public float MovespeedScale = 1f;
    [SerializeField] private float jumpPower = 800f;
    [SerializeField] private float jumpPowerScaleOnLadder = 1f;
    [SerializeField] private float verticalMovespeed = 3f;
    private float jumpDirection;

    private Mover mover;
    private Fighter fighter;
    private Climb climb;
    private Jump jump;
    private PlayerSkill1 playerSkill1;
    private PlayerSkill2 playerSkill2;
    private Rigidbody2D rb;
    private Animator anim;

    private enum State { Idle, MoveHorizontal, MoveVertical, InTheAir, Attack, Skill2 }
    private State playerState = State.Idle;
    #endregion

    private void Start()
    {
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();
        climb = GetComponent<Climb>();
        jump = GetComponent<Jump>();
        playerSkill1 = GetComponent<PlayerSkill1>();
        playerSkill2 = GetComponent<PlayerSkill2>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveHorizontal(Input.GetAxisRaw("Horizontal"));
        MoveVertical(Input.GetAxisRaw("Vertical"));
        if (Input.GetAxisRaw("Fire1") != 0f) Attack();
        if (Input.GetAxisRaw("Fire2") != 0f) Skill1();
        if (Input.GetAxisRaw("Fire3") != 0f) Skill2();
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    #region Events
    private void MoveHorizontal(float direction)
    {
        switch (playerState)
        {
            case State.Idle:
                if (direction != 0f) StateMoveHorizontal();
                break;
            case State.MoveHorizontal:
                if (direction == 0f) StateIdle();
                else mover.MoveTo(direction, movespeed * MovespeedScale);
                break;
            case State.MoveVertical:
                jumpDirection = direction;
                break;
            case State.InTheAir:
                mover.MoveInAirTo(direction, movespeedInTheAir);
                return;
        }
    }

    private void MoveVertical(float direction)
    {
        bool unitCanClimbOnLadder = (direction != 0f && !climb.LadderEnd(direction) && climb.CanClimb);
        switch (playerState)
        {
            case State.Idle:
                if (unitCanClimbOnLadder)
                {
                    StateMoveVertical();
                }
                break;
            case State.MoveHorizontal:
                if (unitCanClimbOnLadder)
                {
                    mover.Cancel();
                    StateMoveVertical();
                }
                break;
            case State.MoveVertical:
                climb.TryToClimb(direction, verticalMovespeed);
                break;
            case State.InTheAir:
                if (unitCanClimbOnLadder)
                {
                    StateMoveVertical();
                }
                break;
        }
    }

    public override void Attack()
    {
        switch (playerState)
        {
            case State.Idle:
                fighter.StartAttack();
                StateAttack();
                break;
            case State.MoveHorizontal:
                fighter.StartAttack();
                mover.Cancel();
                StateAttack();
                break;
        }
    }

    private void Skill1()
    {
        switch (playerState)
        {
            case State.Idle:
                if (!playerSkill1.CanUseSkill) return;
                playerSkill1.StartUse();
                StateAttack();
                break;
            case State.MoveHorizontal:
                if (!playerSkill1.CanUseSkill) return;
                playerSkill1.StartUse();
                mover.Cancel();
                StateAttack();
                break;
        }
    }

    private void Skill2()
    {
        switch (playerState)
        {
            case State.Idle:
                if (!playerSkill2.CanUseSkill) return;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
                playerSkill2.StartUse();
                StateSkill2();
                break;
            case State.MoveHorizontal:
                mover.Cancel();
                if (!playerSkill2.CanUseSkill) return;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
                playerSkill2.StartUse();
                StateSkill2();
                break;
            case State.InTheAir:
                if (!playerSkill2.CanUseSkill) return;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
                playerSkill2.StartUse();
                StateSkill2();
                break;
        }
    }

    private void Jump()
    {
        switch(playerState)
        {
            case State.Idle:
                jump.ToJump(Vector2.up, jumpPower);
                StateInTheAir();
                break;
            case State.MoveHorizontal:
                jump.ToJump(Vector2.up, jumpPower);
                anim.SetBool("moveHorizontal", false);
                StateInTheAir();
                break;
            case State.MoveVertical:
                climb.StopVerticalMove();
                jump.ToJumpOnLadder(new Vector2(0.5f * jumpDirection, 0.5f), jumpPower * jumpPowerScaleOnLadder);
                break;
        }
    }

    public override void Landed()
    {
        StateIdle();
    }

    public override void DisableState()
    {
        StateIdle();
    }

    public override void Fell()
    {
        switch (playerState)
        {
            case State.Idle:
                StateInTheAir();
                break;
            case State.MoveHorizontal:
                mover.Cancel();
                StateInTheAir();
                break;
            case State.Attack:
                StateInTheAir();
                break;
        }
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
        SetState(State.MoveHorizontal);
    }

    private void StateMoveVertical()
    {
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
        playerState = value;
    }
}
