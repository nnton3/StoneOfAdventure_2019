using UnityEngine;
using System;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Movement;

public class PlayerIdleState : MonoBehaviour, IPlayerState
{
    private Fighter fighter;
    private Jump jump;
    private PlayerStateController unit;
    private Climb climb;
    private PlayerMoveHorizontalState moveHorizontalState;
    private PlayerMoveVerticalState moveVerticalState;
    private PlayerAttackState attackState;
    private PlayerJumpState jumpState;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
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
}
