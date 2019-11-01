using UnityEngine;
using StoneOfAdventure.Movement;
using System;
using UnityEngine.Tilemaps;

public class PlayerMoveVerticalState : BaseState
{
    private PlayerStateController unit;
    private Climb climb;
    private Jump jump;
    private BaseState jumpState;
    [SerializeField] private float jumpPowerScaleOnLadder = 1f;

    private void Start()
    {
        unit = GetComponent<PlayerStateController>();
        climb = GetComponent<Climb>();
        jump = GetComponent<Jump>();
        jumpState = GetComponent<PlayerJumpState>();
    }

    public override void Jump(float jumpPower)
    {
        //if (InPlatform()) return;
        climb.StopVerticalMove();
        unit.State = jumpState;
        jump.ToJumpOnLadder(new Vector2(0.5f * jumpDirection, 0.5f), jumpPower * jumpPowerScaleOnLadder);
    }

    [SerializeField] private Tilemap platformMap;
    private bool InPlatform()
    {
        return platformMap.GetTile(platformMap.WorldToCell(transform.position + Vector3.up)) != null;
    }

    private float jumpDirection;
    public override void MoveHorizontal(float direction, float movespeed) { jumpDirection = direction; }

    public override void MoveVertical(float direction, float verticalMovespeed)
    {
        climb.TryToClimb(direction, verticalMovespeed);
    }
}
