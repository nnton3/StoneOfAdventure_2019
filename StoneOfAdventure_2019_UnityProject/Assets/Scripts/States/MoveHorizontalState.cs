using UnityEngine;
using System.Collections;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Core;
using StoneOfAdventure.Combat;

public class MoveHorizontalState : MonoBehaviour, IUnitState
{
    private Animator anim;
    private Fighter fighter;
    private Mover mover;
    private Jump jump;
    private Unit unit;
    private Climb climb;
    private IdleState idleState;
    private AttackState attackState;
    private JumpState jumpState;
    private MoveVerticalState moveVerticalState;

    private void Start()
    {
        anim = GetComponent<Animator>();
        fighter = GetComponent<Fighter>();
        mover = GetComponent<Mover>();
        jump = GetComponent<Jump>();
        unit = GetComponent<Unit>();
        climb = GetComponent<Climb>();
        idleState = GetComponent<IdleState>();
        attackState = GetComponent<AttackState>();
        jumpState = GetComponent<JumpState>();
        moveVerticalState = GetComponent<MoveVerticalState>();
    }

    public void Attack()
    {
        fighter.StartAttack();
        mover.Cancel();
        unit.State = attackState;
    }

    public void Idle() { return; }

    public void Jump(float jumpPower)
    {
        unit.State = jumpState;
        jump.ToJump(Vector2.up, jumpPower);
        anim.SetBool("moveHorizontal", false);
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

    public void MoveVertical(float direction, float verticalMovespeed)
    {
        bool unitCanClimbOnLadder = (direction != 0f && !climb.LadderEnd(direction) && climb.CanClimb);
        if (unitCanClimbOnLadder)
        {
            mover.Cancel();
            unit.State = moveVerticalState;
        }
    }

    public void Fell()
    {
        unit.State = jumpState;
    }
}
