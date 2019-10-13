using UnityEngine;
using StoneOfAdventure.Combat;

public class PlayerAttackState : MonoBehaviour, IPlayerState
{
    private Fighter fighter;
    private PlayerStateController unit;
    private PlayerJumpState jumpState;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
        unit = GetComponent<PlayerStateController>();
        jumpState = GetComponent<PlayerJumpState>();
    }

    public void Attack() { return; }

    public void Idle() { return; }

    public void Jump(float jumpPower) { return; }

    public void MoveHorizontal(float direction, float movespeed) { return; }

    public void MoveVertical(float direction, float verticalMovespeed) { return; }

    public void Fell() { unit.State = jumpState; }
}
