using UnityEngine;
using StoneOfAdventure.Movement;

public class PlayerJumpState : MonoBehaviour, IPlayerState
{
    private PlayerStateController unit;
    private Rigidbody2D rb;
    private Mover mover;
    private Climb climb;
    private Jump jump;
    private PlayerSkill2 playerSkill2;
    private PlayerMoveVerticalState moveVerticalState;

    private void Start()
    {
        unit = GetComponent<PlayerStateController>();
        rb = GetComponent<Rigidbody2D>();
        mover = GetComponent<Mover>();
        climb = GetComponent<Climb>();
        jump = GetComponent<Jump>();
        playerSkill2 = GetComponent<PlayerSkill2>();
        moveVerticalState = GetComponent<PlayerMoveVerticalState>();
    }

    public void Attack() { return; }

    public void Idle() { return; }

    public void Jump(float jumpPower) { return; }

    public void MoveHorizontal(float direction, float movespeed)
    {
        mover.MoveInAirTo(direction, movespeed);
    }

    public void MoveVertical(float direction, float verticalMovespeed)
    {
        bool unitCanClimbOnLadder = (direction != 0f && !climb.LadderEnd(direction) && climb.CanClimb);
        if (unitCanClimbOnLadder)
        {
            jump.Cancel();
            unit.State = moveVerticalState;
        }
    }

    public void Fell() { return; }

    public void Skill1() { return; }

    public void Skill2()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        playerSkill2.StartUse();
    }
}
