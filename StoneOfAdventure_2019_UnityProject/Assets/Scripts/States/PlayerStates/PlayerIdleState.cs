using UnityEngine;
using System;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Movement;

public class PlayerIdleState : BaseState
{
    #region Variables
    private Rigidbody2D rb;
    private Fighter fighter;
    private PlayerSkill1 playerSkill1;
    private PlayerSkill2 playerSkill2;
    private Jump jump;
    private PlayerStateController unit;
    private Climb climb;

    private BaseState moveHorizontalState;
    private BaseState moveVerticalState;
    private BaseState attackState;
    private BaseState jumpState;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fighter = GetComponent<Fighter>();
        playerSkill1 = GetComponent<PlayerSkill1>();
        playerSkill2 = GetComponent<PlayerSkill2>();
        jump = GetComponent<Jump>();
        unit = GetComponent<PlayerStateController>();
        climb = GetComponent<Climb>();
        moveHorizontalState = GetComponent<PlayerMoveHorizontalState>();
        moveVerticalState = GetComponent<PlayerMoveVerticalState>();
        attackState = GetComponent<PlayerAttackState>();
        jumpState = GetComponent<PlayerJumpState>();
    }

    public override void Attack()
    {
        fighter.StartAttack();
        unit.State = attackState;
    }

    public override void Jump(float jumpPower)
    {
        unit.State = jumpState;
        jump.ToJump(Vector2.up, jumpPower);  
    }

    public override void MoveHorizontal(float direction, float movespeed)
    {
        if (direction != 0f) unit.State = moveHorizontalState;
    }

    public override void MoveVertical(float direction, float verticalMovespeed)
    {
        bool unitCanClimbOnLadder = (direction != 0f && !climb.LadderEnd(direction) && climb.CanClimb);
        if (unitCanClimbOnLadder) unit.State = moveVerticalState;
    }

    public override void Fell()
    {
        unit.State = jumpState;
    }

    public override void Skill1()
    {
        playerSkill1.StartUse();
        unit.State = attackState;
    }

    public override void Skill2()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
        playerSkill2.StartUse();
        unit.State = attackState;
    }
}
