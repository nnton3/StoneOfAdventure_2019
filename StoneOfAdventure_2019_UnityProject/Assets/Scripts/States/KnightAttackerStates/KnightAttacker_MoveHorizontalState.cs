using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Movement;

public class KnightAttacker_MoveHorizontalState : MonoBehaviour, IKnightAttackerState
{
    #region
    private Fighter fighter;
    private Mover mover;
    private KnightAttacker_StateController unit;
    private Animator anim;
    private Stunned stunned;
    private Spurt spurt;

    private KnightAttacker_AttackState attackState;
    private KnightAttacker_DeathState deathState;
    private KnightAttacker_StunState stunState;
    private KnightAttacker_SpurtState spurtState;
    #endregion
    private void Start()
    {
        fighter = GetComponent<Fighter>();
        mover = GetComponent<Mover>();
        unit = GetComponent<KnightAttacker_StateController>();
        anim = GetComponent<Animator>();
        stunned = GetComponent<Stunned>();
        spurt = GetComponent<Spurt>();

        attackState = GetComponent<KnightAttacker_AttackState>();
        deathState = GetComponent<KnightAttacker_DeathState>();
        stunState = GetComponent<KnightAttacker_StunState>();
        spurtState = GetComponent<KnightAttacker_SpurtState>();
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

    public void Spurt()
    {
        mover.Cancel();
        unit.State = spurtState;
        spurt.StartAction();
    }
}
