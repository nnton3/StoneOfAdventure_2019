using System;
using UnityEngine;
using StoneOfAdventure.Core;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Combat;
using UnityEngine.Events;

public class PlayerStateController : Unit
{
    #region Variables
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
    private PlayerUltimateSkill ultimateSkill;
    private Rigidbody2D rb;
    private Animator anim;

    [HideInInspector] public UnityEvent StartWalk;
    [HideInInspector] public UnityEvent StopWalk;
    #endregion

    private void Start()
    {
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();
        climb = GetComponent<Climb>();
        jump = GetComponent<Jump>();
        playerSkill1 = GetComponent<PlayerSkill1>();
        playerSkill2 = GetComponent<PlayerSkill2>();
        ultimateSkill = GetComponent<PlayerUltimateSkill>();
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
        if (Input.GetAxisRaw("UltimateSkill") != 0f) UltimateSkill();
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
            case State.MoveVertical:
                jumpDirection = direction;
                break;
            case State.InTheAir:
                mover.MoveInAirTo(direction);
                return;
        }
    }

    public override void MoveVertical(float direction)
    {
        bool unitCanClimbOnLadder = (direction != 0f && !climb.LadderEnd(direction) && climb.CanClimb);
        switch (currentState)
        {
            case State.Idle:
                if (unitCanClimbOnLadder)
                    StateMoveVertical();
                break;
            case State.MoveHorizontal:
                if (unitCanClimbOnLadder)
                    StateMoveVertical();
                break;
            case State.MoveVertical:
                climb.TryToClimb(direction, verticalMovespeed);
                break;
            case State.InTheAir:
                if (unitCanClimbOnLadder)
                    StateMoveVertical();
                break;
        }
    }

    public override void Attack()
    {
        switch (currentState)
        {
            case State.Idle:
                fighter.StartAttack();
                StateAttack();
                break;
            case State.MoveHorizontal:
                fighter.StartAttack();
                StateAttack();
                break;
        }
    }

    public override void Skill1()
    {
        switch (currentState)
        {
            case State.Idle:
                TryToStartSkill1();
                break;
            case State.MoveHorizontal:
                TryToStartSkill1();
                break;
        }

        void TryToStartSkill1()
        {
            if (!playerSkill1.CanUseSkill) return;
            playerSkill1.StartUse();
            StateAttack();
        }
    }

    public override void Skill2()
    {
        switch (currentState)
        {
            case State.Idle:
                TryToStartScill2();
                break;
            case State.MoveHorizontal:
                TryToStartScill2();
                break;
            case State.InTheAir:
                TryToStartScill2();
                break;
        }

        void TryToStartScill2()
        {
            if (!playerSkill2.CanUseSkill) return;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
            playerSkill2.StartUse();
            StateSkill2();
        }
    }

    public override void UltimateSkill()
    {
        if (ultimateSkill.CanUseSkill) ultimateSkill.StartUse();
    }

    public override void Jump()
    {
        switch (currentState)
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
                StateInTheAir();
                break;
        }
    }

    public override void Landed()
    {
        if (currentState == State.InTheAir) anim.SetTrigger("landed");
        StateIdle();
    }

    public override void DisableState()
    {
        StateIdle();
    }

    public override void Fell()
    {
        switch (currentState)
        {
            case State.Idle:
                StateInTheAir();
                break;
            case State.MoveHorizontal:
                anim.SetBool("moveHorizontal", false);
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
        if (currentState == State.MoveHorizontal)
        {
            StopWalk.Invoke();
            mover.CancelMove();
        }
        SetState(State.Idle);
    }

    private void StateMoveHorizontal()
    {
        StartWalk.Invoke();
        SetState(State.MoveHorizontal);
    }

    private void StateMoveVertical()
    {
        if (currentState == State.MoveHorizontal) mover.CancelMove();
        SetState(State.MoveVertical);
    }

    private void StateInTheAir()
    {
        if (currentState == State.MoveHorizontal) StopWalk.Invoke();
        SetState(State.InTheAir);
    }

    private void StateAttack()
    {
        if (currentState == State.MoveHorizontal) mover.CancelMove();
        SetState(State.Attack);
    }

    private void StateSkill2()
    {
        if (currentState == State.MoveHorizontal) mover.CancelMove();
        SetState(State.Skill2);
    }
    #endregion

    private void SetState(State value)
    {
        currentState = value;
    }

    private void OnDisable()
    {
        StartWalk.RemoveAllListeners();
        StopWalk.RemoveAllListeners();
    }
}
