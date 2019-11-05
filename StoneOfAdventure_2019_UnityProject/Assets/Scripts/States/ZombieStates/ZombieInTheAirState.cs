using UnityEngine;
using System.Collections;
using StoneOfAdventure.Movement;

public class ZombieInTheAirState : BaseState
{
    #region Variables
    private Mover mover;
    #endregion

    private void Start()
    {
        mover = GetComponent<Mover>();
    }

    public override void MoveHorizontal(float direction, float movespeed)
    {
        mover.MoveInAirTo(direction, movespeed);
    }
}
