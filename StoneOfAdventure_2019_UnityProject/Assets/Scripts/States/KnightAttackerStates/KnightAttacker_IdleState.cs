using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Core;

public class KnightAttacker_IdleState : MonoBehaviour, IKnightAttackerState
{
    #region Variables
    private Animator anim;
    private KnightAttacker_StateController unit;
    private Fighter fighter;
    private Mover mover;
    private Stunned stunned;
    private Spurt spurt;

    private KnightAttacker_AttackState attackState;
    private KnightAttacker_MoveHorizontalState moveHorizontalState;
    private KnightAttacker_DeathState deathState;
    private KnightAttacker_StunState stunState;
    private KnightAttacker_SpurtState spurtState;
    #endregion
    private void Start()
    {
        anim = GetComponent<Animator>();
        unit = GetComponent<KnightAttacker_StateController>();
        fighter = GetComponent<Fighter>();
        mover = GetComponent<Mover>();
        stunned = GetComponent<Stunned>();
        spurt = GetComponent<Spurt>();

        attackState = GetComponent<KnightAttacker_AttackState>();
        moveHorizontalState = GetComponent<KnightAttacker_MoveHorizontalState>();
        deathState = GetComponent<KnightAttacker_DeathState>();
        stunState = GetComponent<KnightAttacker_StunState>();
        spurtState = GetComponent<KnightAttacker_SpurtState>();
    }

    public void Attack()
    {
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
        unit.State = moveHorizontalState;
    }

    public void Dead()
    {
        anim.SetTrigger("dead");
        unit.State = deathState;
    }

    public void Stun(float time)
    {
        unit.State = stunState;
        stunned.ApplyStun(time);
    }

    public void Spurt()
    {
        unit.State = spurtState;
        spurt.StartAction();
    }
}
