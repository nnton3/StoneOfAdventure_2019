﻿using UnityEngine;
using System.Collections;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Combat;

public class PlayerMoveHorizontalState : BaseState
{
    #region Variables
    private Animator anim;
    private Rigidbody2D rb;
    private Fighter fighter;
    private Mover mover;
    private Jump jump;
    private PlayerStateController unit;
    private Climb climb;
    private PlayerSkill1 playerSkill1;
    private PlayerSkill2 playerSkill2;

    private BaseState attackState;
    private BaseState jumpState;
    private BaseState moveVerticalState;
    private BaseState skill2State;
    #endregion
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fighter = GetComponent<Fighter>();
        playerSkill1 = GetComponent<PlayerSkill1>();
        playerSkill2 = GetComponent<PlayerSkill2>();
        mover = GetComponent<Mover>();
        jump = GetComponent<Jump>();
        unit = GetComponent<PlayerStateController>();
        climb = GetComponent<Climb>();

        attackState = GetComponent<PlayerAttackState>();
        jumpState = GetComponent<PlayerJumpState>();
        moveVerticalState = GetComponent<PlayerMoveVerticalState>();
        skill2State = GetComponent<PlayerSkill2State>();
    }

    public override void Attack()
    {
        fighter.StartAttack();
        mover.Cancel();
        unit._State = attackState;
    }

    public override void Jump(float jumpPower)
    {
        unit._State = jumpState;
        jump.ToJump(Vector2.up, jumpPower);
        anim.SetBool("moveHorizontal", false);
    }

    public override void MoveHorizontal(float direction, float movespeed)
    {
        if (direction == 0f)
        {
            unit.DisableState();
            mover.Cancel();
            return;
        }
        mover.MoveTo(direction, movespeed);
    }

    public override void MoveVertical(float direction, float verticalMovespeed)
    {
        bool unitCanClimbOnLadder = (direction != 0f && !climb.LadderEnd(direction) && climb.CanClimb);
        if (unitCanClimbOnLadder)
        {
            mover.Cancel();
            unit._State = moveVerticalState;
        }
    }

    public override void Fell()
    {
        mover.Cancel();
        unit._State = jumpState;
    }

    public override void Skill1()
    {
        if (!playerSkill1.CanUseSkill) return;
        playerSkill1.StartUse();
        mover.Cancel();
        unit._State = attackState;
    }

    public override void Skill2()
    {
        if (!playerSkill2.CanUseSkill) return;
        unit._State = skill2State;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
        playerSkill2.StartUse();
    }
}
