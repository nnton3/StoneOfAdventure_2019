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
    private MoveHorizontalState movehHrizontalState;
    private AttackState attackState;
    private JumpState jumpState;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
        jump = GetComponent<Jump>();
        unit = GetComponent<Unit>();
        movehHrizontalState = GetComponent<MoveHorizontalState>();
        attackState = GetComponent<AttackState>();
        jumpState = GetComponent<JumpState>();
    }

    public void Attack()
    {
        fighter.StartAttack();
        unit.State = attackState;
    }

    public void Idle()
    {
        return;
    }

    public void Jump(float jumpPower)
    {
        unit.State = jumpState;
        jump.ToJump(jumpPower);  
    }

    public void MoveHorizontal(float direction, float movespeed)
    {
        if (direction != 0f) unit.State = movehHrizontalState;
    }

    public void MoveVertical()
    {
        throw new NotImplementedException();
    }

    public void Fell()
    {
        unit.State = jumpState;
    }
}
