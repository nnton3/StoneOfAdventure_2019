using UnityEngine;
using System;
using StoneOfAdventure.Core;
using StoneOfAdventure.Combat;

public class IdleState : MonoBehaviour, IUnitState
{
    private Fighter fighter;
    private Unit unit;
    private MoveHorizontalState movehHrizontalState;
    private AttackState attackState;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
        unit = GetComponent<Unit>();
        movehHrizontalState = GetComponent<MoveHorizontalState>();
        attackState = GetComponent<AttackState>();
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

    public void Jump()
    {
        throw new NotImplementedException();
    }

    public void MoveHorizontal(float direction, float movespeed)
    {
        if (direction != 0f) unit.State = movehHrizontalState;
    }

    public void MoveVertical()
    {
        throw new NotImplementedException();
    }
}
