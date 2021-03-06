﻿using UnityEngine;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Combat;
using Zenject;
using UniRx;
using UniRx.Triggers;

public class PlayerStateController : StoneOfAdventure.Core.Unit
{
    #region Variables
    [SerializeField] private float jumpPower = 800f;
    [SerializeField] private float jumpPowerScaleOnLadder = 1f;
    [SerializeField] private float verticalMovespeed = 3f;
    [SerializeField] private ContactFilter2D filter;

    [Inject] private SignalBus signalBus;

    [Inject(Id = "Player")] private Mover mover;
    [Inject(Id = "Player")] private Fighter fighter;
    [Inject(Id = "Player")] private Climb climb;
    [Inject(Id = "Player")] private Jump jump;
    [Inject(Id = "Player")] private PlayerSkill1 playerSkill1;
    [Inject(Id = "Player")] private PlayerSkill2 playerSkill2;
    [Inject(Id = "Player")] private PlayerUltimateSkill ultimateSkill;
    [Inject(Id = "Player")] private Rigidbody2D rb;
    [Inject(Id = "Player")] private Animator anim;
    private float jumpDirection;
    #endregion

    private void Start()
    {
        signalBus.Subscribe<PlayerStartNextLevel>(PlayerStartNextLvl);

        BindInput();
    }

    private void BindInput()
    {
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                MoveHorizontal(Input.GetAxisRaw("Horizontal"));
                MoveVertical(Input.GetAxisRaw("Vertical"));
            });
        this.UpdateAsObservable()
            .Where(_ => Input.GetAxisRaw("Fire1") != 0f)
            .Subscribe(_ => Attack());
        this.UpdateAsObservable()
           .Where(_ => Input.GetAxisRaw("Fire2") != 0f)
           .Subscribe(_ => Skill1());
        this.UpdateAsObservable()
           .Where(_ => Input.GetAxisRaw("Fire3") != 0f)
           .Subscribe(_ => Skill2());
        this.UpdateAsObservable()
           .Where(_ => Input.GetAxisRaw("UltimateSkill") != 0f)
           .Subscribe(_ => UltimateSkill());
        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.Space))
            .Subscribe(_ => Jump());
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
                TryToUseSkill();
                break;
            case State.MoveHorizontal:
                TryToUseSkill();
                break;
        }

        void TryToUseSkill()
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
                TryToUseSkill();
                break;
            case State.MoveHorizontal:
                TryToUseSkill();
                break;
            case State.InTheAir:
                TryToUseSkill();
                break;
        }

        void TryToUseSkill()
        {
            if (!playerSkill2.CanUseSkill) return;
            StateSkill2();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
            playerSkill2.StartUse();
        }
    }

    public override void UltimateSkill()
    {
        switch (currentState)
        {
            case State.Idle:
                TryToStartUltimate();
                break;
            case State.MoveHorizontal:
                TryToStartUltimate();
                break;
        }

        void TryToStartUltimate()
        {
            if (!ultimateSkill.CanUseSkill) return;
            ultimateSkill.StartUse();
            StateAttack();
        }
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
                if (!CollisionWithPlatform())
                {
                    climb.StopVerticalMove();
                    jump.ToJumpOnLadder(new Vector2(0.5f * jumpDirection, 0.5f), jumpPower * jumpPowerScaleOnLadder);
                    StateInTheAir();
                }
                break;
        }
    }

    public override void Landed()
    {
        if (currentState == State.InTheAir) anim.SetTrigger("landed");
        StateIdle();
    }

    public override void ApplyStun(float time)
    {
        if (currentState == State.Skill2)
        {
            playerSkill2.Skill2End();
        }

        anim.SetTrigger("stun");
        StateStun();
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
            case State.Skill2:
                if (!(rb.constraints == RigidbodyConstraints2D.FreezeRotation)) return;
                StateInTheAir();
                break;
        }
    }

    private void PlayerStartNextLvl()
    {
        anim.SetTrigger("startNextLvl");
        StateAttack();
    }
    #endregion

    #region StateTransitions
    private void StateIdle()
    {
        switch (currentState)
        {
            case State.MoveHorizontal:
                mover.CancelMove();
                break;
        }
        SetState(State.Idle);
    }

    private void StateMoveHorizontal()
    {
        if (currentState == State.InTheAir) anim.ResetTrigger("landed");

        signalBus.Fire<PlayerStartWalk>();
        SetState(State.MoveHorizontal);
    }

    private void StateMoveVertical()
    {
        if (currentState == State.MoveHorizontal) mover.CancelMove();
        SetState(State.MoveVertical);
    }

    private void StateInTheAir()
    {
        switch (currentState)
        {
            case State.MoveHorizontal:
                anim.SetTrigger("jump");
                break;
            case State.Skill2:
                anim.SetTrigger("skill2end");
                break;
        }
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

    private void StateStun()
    {
        if (currentState == State.MoveHorizontal) mover.CancelMove();
        SetState(State.Stun);
    }
    #endregion

    private void SetState(State value)
    {
        if (currentState == State.MoveHorizontal) signalBus.Fire<PlayerStopWalk>();
        currentState = value;
    }
    
    private bool CollisionWithPlatform()
    {
        return GetComponent<BoxCollider2D>().IsTouching(filter);
    }
}
