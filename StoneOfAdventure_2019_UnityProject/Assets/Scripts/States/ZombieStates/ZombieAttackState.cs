using UnityEngine;
using System.Collections;

public class ZombieAttackState : MonoBehaviour, IZombieState
{
    private Animator anim;
    private ZombieStateController unit;
    private ZombieFighter fighter;
    private Stunned stunned;

    private ZombieDeathState deathState;
    private ZombieStunState stunState;

    private void Start()
    {
        anim = GetComponent<Animator>();
        unit = GetComponent<ZombieStateController>();
        fighter = GetComponent<ZombieFighter>();
        stunned = GetComponent<Stunned>();

        deathState = GetComponent<ZombieDeathState>();
        stunState = GetComponent<ZombieStunState>();
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
}
