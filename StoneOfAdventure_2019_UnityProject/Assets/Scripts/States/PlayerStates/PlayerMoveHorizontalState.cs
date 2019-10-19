using UnityEngine;
using System.Collections;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Combat;

public class PlayerMoveHorizontalState : MonoBehaviour, IPlayerState
{
    #region Variables
    private Animator anim;
    private Rigidbody2D rb;
    private Fighter fighter;
    private Mover mover;
    private Jump jump;
    private PlayerStateController unit;
    private Climb climb;
    private PlayerAttackState attackState;
    private PlayerJumpState jumpState;
    private PlayerMoveVerticalState moveVerticalState;
    private PlayerSkill1 playerSkill1;
    private PlayerSkill2 playerSkill2;
    #endregion 
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fighter = GetComponent<Fighter>();
        playerSkill1 = GetComponent<PlayerSkill1>();
        playerSkill2 = GetComponent<PlayerSkill2>();
        mover = GetComponent<Mover>();
        jump = GetComponent<Jump>();
        unit = GetComponent<PlayerStateController>();
        climb = GetComponent<Climb>();
        attackState = GetComponent<PlayerAttackState>();
        jumpState = GetComponent<PlayerJumpState>();
        moveVerticalState = GetComponent<PlayerMoveVerticalState>();
    }

    public void Attack()
    {
        fighter.StartAttack();
        mover.Cancel();
        unit.State = attackState;
    }

    public void Idle() { return; }

    public void Jump(float jumpPower)
    {
        unit.State = jumpState;
        jump.ToJump(Vector2.up, jumpPower);
        anim.SetBool("moveHorizontal", false);
    }

    public void MoveHorizontal(float direction, float movespeed)
    {
        if (direction == 0f)
        {
            unit.DisableState();
            mover.Cancel();
            return;
        }
        mover.MoveTo(direction, movespeed);
    }

    public void MoveVertical(float direction, float verticalMovespeed)
    {
        bool unitCanClimbOnLadder = (direction != 0f && !climb.LadderEnd(direction) && climb.CanClimb);
        if (unitCanClimbOnLadder)
        {
            mover.Cancel();
            unit.State = moveVerticalState;
        }
    }

    public void Fell()
    {
        mover.Cancel();
        unit.State = jumpState;
    }

    public void Skill1()
    {
        playerSkill1.StartUse();
        mover.Cancel();
        unit.State = attackState;
    }

    public void Skill2()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
        playerSkill2.StartUse();
        mover.Cancel();
        unit.State = attackState;
    }
}
