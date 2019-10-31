using UnityEngine;
using StoneOfAdventure.Combat;

public class PlayerAttackState : BaseState
{
    private PlayerStateController unit;
    private PlayerJumpState jumpState;

    private void Start()
    {
        unit = GetComponent<PlayerStateController>();
        jumpState = GetComponent<PlayerJumpState>();
    }

    public override void Fell() { unit.State = jumpState; }
}
