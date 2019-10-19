using UnityEngine;
using StoneOfAdventure.Combat;

public class PlayerAttackState : MonoBehaviour, IPlayerState
{
    private PlayerStateController unit;
    private PlayerJumpState jumpState;

    private void Start()
    {
        unit = GetComponent<PlayerStateController>();
        jumpState = GetComponent<PlayerJumpState>();
    }

    public void Attack() { return; }

    public void Idle() { return; }

    public void Jump(float jumpPower) { return; }

    public void MoveHorizontal(float direction, float movespeed) { return; }

    public void MoveVertical(float direction, float verticalMovespeed) { return; }

    public void Fell() { unit.State = jumpState; }

    public void Skill1() { return; }

    public void Skill2() { return; }
}
