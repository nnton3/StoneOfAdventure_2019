using UnityEngine;
using System.Collections;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Core;
using StoneOfAdventure.Combat;

public class MoveHorizontalState : MonoBehaviour, IUnitState
{
    private Fighter fighter;
    private Mover mover;
    private Unit unit;
    private IdleState idleState;
    private AttackState attackState;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
        mover = GetComponent<Mover>();
        unit = GetComponent<Unit>();
        idleState = GetComponent<IdleState>();
        attackState = GetComponent<AttackState>();
    }

    public void Attack()
    {
        fighter.StartAttack();
        mover.Cancel();
        unit.State = attackState;
    }

    public void Idle()
    {
        return;
    }

    public void Jump()
    {
        throw new System.NotImplementedException();
    }

    public void MoveHorizontal(float direction, float movespeed)
    {
        if (direction == 0f)
        {
            unit.State = idleState;
            mover.Cancel();
            return;
        }
        mover.MoveTo(direction, movespeed);
    }

    public void MoveVertical()
    {
        throw new System.NotImplementedException();
    }
}
