using UnityEngine;
using StoneOfAdventure.Core;
using StoneOfAdventure.Combat;

public class AttackState : MonoBehaviour, IUnitState
{
    private Fighter fighter;
    private Unit unit;
    private JumpState jumpState;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
        unit = GetComponent<Unit>();
        jumpState = GetComponent<JumpState>();
    }

    public void Attack() { return; }

    public void Idle() { return; }

    public void Jump(float jumpPower) { return; }

    public void MoveHorizontal(float direction, float movespeed) { return; }

    public void MoveVertical(float direction, float verticalMovespeed) { return; }

    public void Fell() { unit.State = jumpState; }
}
