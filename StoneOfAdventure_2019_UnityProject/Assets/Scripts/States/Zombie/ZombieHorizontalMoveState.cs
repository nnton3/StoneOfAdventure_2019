using UnityEngine;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Combat;

public class ZombieHorizontalMoveState : MonoBehaviour, IZombieState
{
    private Fighter fighter;
    private Mover mover;
    private ZombieStateController unit;
    private Animator anim;

    private ZombieAttackState attackState;
    private ZombieDeathState deathState;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
        mover = GetComponent<Mover>();
        unit = GetComponent<ZombieStateController>();
        anim = GetComponent<Animator>();

        attackState = GetComponent<ZombieAttackState>();
    }

    public void Attack()
    {
        mover.Cancel();
        unit.State = attackState;
        fighter.StartAttack();
    }

    public void MoveHorizontal(float direction, float movespeed)
    {
        if (direction == 0f)
        {
            unit.DisableState();
            mover.Cancel();
            return;
        }
        mover.MoveTo(direction, movespeed);
    }

    public void Dead()
    {
        anim.SetTrigger("dead");
        unit.State = deathState;
    }
}
