using UnityEngine;
using StoneOfAdventure.Movement;

public class JumpState : MonoBehaviour, IUnitState
{
    private Mover mover;

    private void Start()
    {
        mover = GetComponent<Mover>();
    }

    public void Attack() { return; }

    public void Idle() { return; }

    public void Jump(float jumpPower) { return; }

    public void MoveHorizontal(float direction, float movespeed)
    {
        mover.MoveInJumpTo(direction, movespeed);
    }

    public void MoveVertical()
    {
        throw new System.NotImplementedException();
    }

    public void Fell() { return; }
}
