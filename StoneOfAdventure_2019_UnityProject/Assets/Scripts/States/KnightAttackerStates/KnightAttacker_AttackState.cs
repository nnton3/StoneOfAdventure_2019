using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;

public class KnightAttacker_AttackState : MonoBehaviour, IKnightAttackerState
{
    #region Variables
    private Animator anim;
    private KnightAttacker_StateController unit;
    private Fighter fighter;
    private Stunned stunned;

    private KnightAttacker_DeathState deathState;
    private KnightAttacker_StunState stunState;
    #endregion
    private void Start()
    {
        anim = GetComponent<Animator>();
        unit = GetComponent<KnightAttacker_StateController>();
        fighter = GetComponent<Fighter>();
        stunned = GetComponent<Stunned>();

        deathState = GetComponent<KnightAttacker_DeathState>();
        stunState = GetComponent<KnightAttacker_StunState>();
    }

    public void Attack() { return; }

    public void Dead()
    {
        anim.SetTrigger("dead");
        unit.State = deathState;
    }

    public void MoveHorizontal(float direction, float movespeed) { return; }

    public void Stun(float time)
    {
        fighter.Cancel();
        unit.State = stunState;
        stunned.ApplyStun(time);
    }

    public void Spurt()
    {
        return;
    }
}
