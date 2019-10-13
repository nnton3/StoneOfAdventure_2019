using UnityEngine;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Core;

public class JumpState : MonoBehaviour, IUnitState
{
    private Unit unit;
    private Mover mover;
    private MoveVerticalState moveVerticalState;

    private void Start()
    {
        unit = GetComponent<Unit>();
        mover = GetComponent<Mover>();
        moveVerticalState = GetComponent<MoveVerticalState>();
    }

    public void Attack() { return; }

    public void Idle() { return; }

    public void Jump(float jumpPower) { return; }

    public void MoveHorizontal(float direction, float movespeed)
    {
        mover.MoveInJumpTo(direction, movespeed);
    }

    public void MoveVertical(float direction, float verticalMovespeed)
    {
        if (direction != 0f) unit.State = moveVerticalState;
    }

    public void Fell() { return; }
}
