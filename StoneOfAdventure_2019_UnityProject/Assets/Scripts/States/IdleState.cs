using UnityEngine;
using System;
using StoneOfAdventure.Core;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Movement;

public class IdleState : MonoBehaviour, IUnitState
{
    private Fighter fighter;
    private Jump jump;
    private Unit unit;
    private Climb climb;
    private MoveHorizontalState moveHorizontalState;
    private MoveVerticalState moveVerticalState;
    private AttackState attackState;
    private JumpState jumpState;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
        jump = GetComponent<Jump>();
        unit = GetComponent<Unit>();
        climb = GetComponent<Climb>();
        moveHorizontalState = GetComponent<MoveHorizontalState>();
        moveVerticalState = GetComponent<MoveVerticalState>();
        attackState = GetComponent<AttackState>();
        jumpState = GetComponent<JumpState>();
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
        if (direction != 0f && !climb.LadderEnd(direction) && climb.CanClimb) unit.State = moveVerticalState;
    }

    public void Fell()
    {
        unit.State = jumpState;
    }
}
