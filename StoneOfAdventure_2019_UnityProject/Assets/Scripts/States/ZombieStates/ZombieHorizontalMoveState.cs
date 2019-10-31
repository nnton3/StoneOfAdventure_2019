using UnityEngine;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;

public class ZombieHorizontalMoveState : BaseState
{
    #region Variables
    private Fighter fighter;
    private Mover mover;
    private Unit unit;
    private Animator anim;
    private Stunned stunned;

    private BaseState attackState;
    private BaseState deathState;
    private BaseState stunState;
    #endregion
    private void Start()
    {
        fighter = GetComponent<Fighter>();
        mover = GetComponent<Mover>();
        unit = GetComponent<Unit>();
        anim = GetComponent<Animator>();
        stunned = GetComponent<Stunned>();

        attackState = GetComponent<ZombieAttackState>();
        deathState = GetComponent<ZombieDeathState>();
        stunState = GetComponent<ZombieStunState>();
    }

    public override void Attack()
    {
        mover.Cancel();
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
        mover.MoveTo(direction, movespeed);
    }

    public override void Dead()
    {
        mover.Cancel();
        anim.SetTrigger("dead");
        unit.State = deathState;
    }

    public override void Stun(float time)
    {
        mover.Cancel();
        unit.State = stunState;
        stunned.ApplyStun(time);
    }
}
