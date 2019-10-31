using UnityEngine;
using System.Collections;

public class ZombieStunState : BaseState
{
    private Animator anim;
    private ZombieStateController unit;
    private Stunned stunned;

    private BaseState deathState;

    private void Start()
    {
        anim = GetComponent<Animator>();
        unit = GetComponent<ZombieStateController>();
        stunned = GetComponent<Stunned>();

        deathState = GetComponent<ZombieDeathState>();
    }

    public override void Dead()
    {
        anim.SetTrigger("dead");
        stunned.Cancel();
        unit.State = deathState;
    }
}
