﻿using StoneOfAdventure.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinSkill3 : SkillBase
{
    #region Variables
    private Animator anim;
    private Transform target;
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerStateController>().transform;
    }

    public override void StartUse()
    {
        base.StartUse();

        anim.SetTrigger("jump");
    }

    public void Teleport()
    {
        var targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = targetPosition;
    }

    // Animation event
    public void Skill3Hit()
    {
        Vector2 centerInRelationUnitDirection =
            transform.position + applicationAreaCenter;
        Collider2D[] enemiesInApplicationArea = Physics2D.OverlapBoxAll(
            centerInRelationUnitDirection,
            applicationArea,
            0f,
            layerMask);
        foreach (var enemie in enemiesInApplicationArea)
        {
            enemie.GetComponent<Health>().ApplyDamage(baseDamage);
        }
    }

    [SerializeField] private Vector3 applicationAreaCenter;
    [SerializeField] private Vector3 applicationArea;
    [SerializeField] private bool applicationAreaVisible;
    [SerializeField] private LayerMask layerMask;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (applicationAreaVisible)
            Gizmos.DrawWireCube(transform.position + applicationAreaCenter, applicationArea);
    }
}