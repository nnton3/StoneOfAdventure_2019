using UnityEngine;
using System;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Movement;

public class PlayerIdleState : MonoBehaviour, IPlayerState
{
    #region Variables
    private Rigidbody2D rb;
    private Fighter fighter;
    private PlayerSkill1 playerSkill1;
    private PlayerSkill2 playerSkill2;
    private Jump jump;
    private PlayerStateController unit;
    private Climb climb;
    private PlayerMoveHorizontalState moveHorizontalState;
    private PlayerMoveVerticalState moveVerticalState;
    private PlayerAttackState attackState;
    private PlayerJumpState jumpState;
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

    public void Attack()
    {
        fighter.StartAttack();
        unit.State = attackState;
    }

    public void Idle() { return; }

    public void Jump(float jumpPower)
    {
        unit.State = jumpState;
        jump.ToJump(Vector2.up, jumpPower);  
    }

    public void MoveHorizontal(float direction, float movespeed)
    {
        if (direction != 0f) unit.State = moveHorizontalState;
    }

    public void MoveVertical(float direction, float verticalMovespeed)
    {
        bool unitCanClimbOnLadder = (direction != 0f && !climb.LadderEnd(direction) && climb.CanClimb);
        if (unitCanClimbOnLadder) unit.State = moveVerticalState;
    }

    public void Fell()
    {
        unit.State = jumpState;
    }

    public void Skill1()
    {
        playerSkill1.StartUse();
        unit.State = attackState;
    }

    public void Skill2()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
        playerSkill2.StartUse();
        unit.State = attackState;
    }
}
