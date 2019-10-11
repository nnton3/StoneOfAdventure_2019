using UnityEngine;
using StoneOfAdventure.Movement;
using System;

public class IdleState : MonoBehaviour, IUnitState
{
    Mover mover;

    private void Start()
    {
        mover = GetComponent<Mover>();
    }

    public void Attack()
    {
        throw new NotImplementedException();
    }

    public void Idle()
    {
        return;
    }

    public void Jump()
    {
        throw new NotImplementedException();
    }

    public void MoveHorizontal(float direction, float movespeed)
    {
        mover.MoveTo(direction, movespeed);
    }

    public void MoveVertical()
    {
        throw new NotImplementedException();
    }
}
