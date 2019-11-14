using UnityEngine;
using StoneOfAdventure.Movement;

public class PlayerJumpState : BaseState
{
    #region Variables
    private PlayerStateController unit;
    private Rigidbody2D rb;
    private Mover mover;
    private Climb climb;
    private Jump jump;
    private PlayerSkill2 playerSkill2;

    private BaseState moveVerticalState;
    private BaseState skill2State;
    #endregion

    private void Start()
    {
        unit = GetComponent<PlayerStateController>();
        rb = GetComponent<Rigidbody2D>();
        mover = GetComponent<Mover>();
        climb = GetComponent<Climb>();
        jump = GetComponent<Jump>();
        playerSkill2 = GetComponent<PlayerSkill2>();

        moveVerticalState = GetComponent<PlayerMoveVerticalState>();
        skill2State = GetComponent<PlayerSkill2State>();
    }

    public override void MoveHorizontal(float direction, float movespeed)
    {
        mover.MoveInAirTo(direction, movespeed);
    }

    public override void MoveVertical(float direction, float verticalMovespeed)
    {
        bool unitCanClimbOnLadder = (direction != 0f && !climb.LadderEnd(direction) && climb.CanClimb);
        if (unitCanClimbOnLadder)
        {
            unit._State = moveVerticalState;
        }
    }

    public override void Skill2()
    {
        if (!playerSkill2.CanUseSkill) return;
        unit._State = skill2State;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        playerSkill2.StartUse();
    }
}
