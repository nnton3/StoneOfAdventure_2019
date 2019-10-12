using UnityEngine;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Core;

public class MoveVerticalState : MonoBehaviour, IUnitState
{
    private Unit unit;
    private Climb climb;
    private Jump jump;
    private JumpState jumpState;
    [SerializeField] private float jumpPowerScaleOnLadder = 1f;

    private void Start()
    {
        unit = GetComponent<Unit>();
        climb = GetComponent<Climb>();
        jump = GetComponent<Jump>();
        jumpState = GetComponent<JumpState>();
    }

    public void Attack() { return; }

    public void Fell()
    {
        unit.State = jumpState;
        climb.StopVerticalMove();
    }

    public void Idle() { return; }

    public void Jump(float jumpPower)
    {
        unit.State = jumpState;
        climb.StopVerticalMove();
        jump.ToJumpOnLadder(new Vector2(0.5f * jumpDirection, 0.5f), jumpPower * jumpPowerScaleOnLadder);
    }

    private float jumpDirection;
    public void MoveHorizontal(float direction, float movespeed) { jumpDirection = direction; }

    public void MoveVertical(float direction, float verticalMovespeed)
    {
        climb.TryToClimb(direction, verticalMovespeed);
    }
}
