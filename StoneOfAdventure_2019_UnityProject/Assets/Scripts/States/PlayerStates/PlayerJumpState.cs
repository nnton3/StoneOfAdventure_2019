using UnityEngine;
using StoneOfAdventure.Movement;

public class PlayerJumpState : MonoBehaviour, IPlayerState
{
    private PlayerStateController unit;
    private Mover mover;
    private Climb climb;
    private PlayerMoveVerticalState moveVerticalState;

    private void Start()
    {
        unit = GetComponent<PlayerStateController>();
        mover = GetComponent<Mover>();
        climb = GetComponent<Climb>();
        moveVerticalState = GetComponent<PlayerMoveVerticalState>();
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
        bool unitCanClimbOnLadder = (direction != 0f && !climb.LadderEnd(direction) && climb.CanClimb);
        if (unitCanClimbOnLadder) unit.State = moveVerticalState;
    }

    public void Fell() { return; }
}
