using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Core;

public class ZombieIdleState : BaseState
{
    #region Variables
    private Animator anim;
    private Unit unit;
    private Fighter fighter;
    private Mover mover;
    private Stunned stunned;

    private BaseState attackState;
    private BaseState moveHorizontalState;
    private BaseState deathState;
    private BaseState stunState;
    #endregion
    private void Start()
    {
        anim = GetComponent<Animator>();
        unit = GetComponent<Unit>();
        fighter = GetComponent<Fighter>();
        mover = GetComponent<Mover>();
        stunned = GetComponent<Stunned>();

        attackState = GetComponent<ZombieAttackState>();
        moveHorizontalState = GetComponent<ZombieHorizontalMoveState>();
        deathState = GetComponent<ZombieDeathState>();
        stunState = GetComponent<ZombieStunState>();
    }

    public override void Attack()
    {
        unit.State = attackState;
        fighter.StartAttack();
    }

    public override void MoveHorizontal(float direction, float movespeed)
    {
        if (direction == 0f)
        {
            unit.DisableState();
            mover.Cancel();
            return;
        }
        unit.State = moveHorizontalState;
    }

    public override void Dead()
    {
        anim.SetTrigger("dead");
        unit.State = deathState;
    }

    public override void Stun(float time)
    {
        unit.State = stunState;
        stunned.ApplyStun(time);
    }
}
