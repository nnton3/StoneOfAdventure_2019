using UnityEngine;
using System.Collections;

public class ZombieStunState : BaseState
{
    #region Variables
    private Animator anim;
    private ZombieStateController unit;
    private Stunned stunned;

    private BaseState deathState;
    private BaseState inTheAirState;
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
        unit = GetComponent<ZombieStateController>();
        stunned = GetComponent<Stunned>();

        deathState = GetComponent<ZombieDeathState>();
        inTheAirState = GetComponent<ZombieInTheAirState>();
    }

    public override void Dead()
    {
        anim.SetTrigger("dead");
        stunned.Cancel();
        unit.State = deathState;
    }

    public override void Fell()
    {
        unit.State = inTheAirState;
    }
}
