﻿using UnityEngine;
using StoneOfAdventure.Combat;
using System.Collections;
using StoneOfAdventure.Core;
using StoneOfAdventure.Movement;

public class PlayerSkill2 : MonoBehaviour
{
    #region Vatiables
    [SerializeField] private float damage;
    [SerializeField] private float movespeed = 10f;
    [SerializeField] private float coolDown = 2f;

    private bool canUseSkill = true;
    public bool CanUseSkill => canUseSkill;
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

    public void StartUse()
    {
        canUseSkill = false;
        StartCoroutine("CoolDownTimer");
        anim.SetTrigger("skill2");
        float direction = (flip.isFacingRight) ? 1f : -1f;
        mover.MoveTo(direction, movespeed);
        playerScill2Collider.SetActive(true);
    }

    private IEnumerator CoolDownTimer()
    {
        yield return new WaitForSeconds(coolDown);
        canUseSkill = true;
    }

    public void Skill2End()
    {
        playerScill2Collider.SetActive(false);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        mover.Cancel();
        unit.DisableState();
        if (!jump.isGrounded) { unit.Fell(); }
    }
}
