using UnityEngine;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Combat;

public class ZombieHorizontalMoveState : MonoBehaviour, IZombieState
{
    private Fighter fighter;
    private Mover mover;
    private ZombieStateController unit;
    private Animator anim;
    private Stunned stunned;

    private ZombieAttackState attackState;
    private ZombieDeathState deathState;
    private ZombieStunState stunState;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
        mover = GetComponent<Mover>();
        unit = GetComponent<ZombieStateController>();
        anim = GetComponent<Animator>();
        stunned = GetComponent<Stunned>();

        attackState = GetComponent<ZombieAttackState>();
        deathState = GetComponent<ZombieDeathState>();
        stunState = GetComponent<ZombieStunState>();
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
        mover.Cancel();
        anim.SetTrigger("dead");
        unit.State = deathState;
    }

    public void Stun(float time)
    {
        mover.Cancel();
        unit.State = stunState;
        stunned.ApplyStun(time);
    }
}
