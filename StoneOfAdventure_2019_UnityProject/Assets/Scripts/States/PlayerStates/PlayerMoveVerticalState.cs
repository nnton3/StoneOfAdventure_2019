using UnityEngine;
using StoneOfAdventure.Movement;

public class PlayerMoveVerticalState : MonoBehaviour, IPlayerState
{
    private PlayerStateController unit;
    private Climb climb;
    private Jump jump;
    private PlayerJumpState jumpState;
    [SerializeField] private float jumpPowerScaleOnLadder = 1f;

    private void Start()
    {
        unit = GetComponent<PlayerStateController>();
        climb = GetComponent<Climb>();
        jump = GetComponent<Jump>();
        jumpState = GetComponent<PlayerJumpState>();
    }

    public void Attack() { return; }

    public void Fell()
    {
        climb.StopVerticalMove();
        unit.State = jumpState;
    }

    public void Idle() { return; }

    public void Jump(float jumpPower)
    {
        climb.StopVerticalMove();
        unit.State = jumpState;
        jump.ToJumpOnLadder(new Vector2(0.5f * jumpDirection, 0.5f), jumpPower * jumpPowerScaleOnLadder);
    }

    private float jumpDirection;
    public void MoveHorizontal(float direction, float movespeed) { jumpDirection = direction; }

    public void MoveVertical(float direction, float verticalMovespeed)
    {
        climb.TryToClimb(direction, verticalMovespeed);
    }
}
