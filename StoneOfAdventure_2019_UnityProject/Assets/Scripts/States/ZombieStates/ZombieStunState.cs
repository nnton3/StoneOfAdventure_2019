using UnityEngine;
using System.Collections;
using StoneOfAdventure.Core;

public class ZombieStunState : BaseState
{
    #region Variables
    private Animator anim;
    private Unit unit;
    private Stunned stunned;

    private BaseState deathState;
    private BaseState inTheAirState;
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
        unit = GetComponent<Unit>();
        stunned = GetComponent<Stunned>();

        deathState = GetComponent<ZombieDeathState>();
        inTheAirState = GetComponent<ZombieInTheAirState>();
    }

    public override void Dead()
    {
        anim.SetTrigger("dead");
        stunned.Cancel();
        unit._State = deathState;
    }

    public override void Fell()
    {
        unit._State = inTheAirState;
    }
}
