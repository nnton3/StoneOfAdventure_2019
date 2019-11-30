using UnityEngine;
using StoneOfAdventure.Combat;
using System.Collections;
using StoneOfAdventure.Core;
using StoneOfAdventure.Movement;

public class PlayerSkill2 : SkillBase
{
    #region Vatiables
    [SerializeField] private float movespeed = 10f;

    private Animator anim;
    private Rigidbody2D rb;
    private PlayerStateController unit;
    private Mover mover;
    private Jump jump;
    private Flip flip;
    private GameObject playerScill2Collider;
    #endregion

    private void Start()
    {
        unit = GetComponent<PlayerStateController>();
        mover = GetComponent<Mover>();
        jump = GetComponent<Jump>();
        flip = GetComponent<Flip>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerScill2Collider = transform.Find("Skill2Collider").gameObject;
        playerScill2Collider.SetActive(false);
    }

    public override void StartUse()
    {
        base.StartUse();
        playerScill2Collider.GetComponent<PlayerSkill2Collider>().skill2Damage = baseDamage;
        anim.SetTrigger("skill2");
    }

    // Animation event
    public void StartMove()
    {
        float direction = (flip.isFacingRight) ? 1f : -1f;
        mover.MoveTo(direction, movespeed);
        playerScill2Collider.SetActive(true);
    }

    // Animation event
    public void Skill2End()
    {
        playerScill2Collider.SetActive(false);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        mover.CancelMove();
        if (!jump.isGrounded)
        {
            unit.Fell();
            return;
        }
        unit.DisableState();
    }
}
