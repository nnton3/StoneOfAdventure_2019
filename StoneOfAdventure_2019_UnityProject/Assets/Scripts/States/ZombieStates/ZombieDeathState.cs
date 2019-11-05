using UnityEngine;
using System.Collections;
using StoneOfAdventure.Core;

public class ZombieDeathState : BaseState
{
    private Unit unit;

    private void Start()
    {
        unit = GetComponent<Unit>();
    }

    public override void Born()
    {
        unit.DisableState();
    }
}
