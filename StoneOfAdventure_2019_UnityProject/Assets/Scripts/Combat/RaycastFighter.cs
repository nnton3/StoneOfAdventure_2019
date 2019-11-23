﻿using StoneOfAdventure.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastFighter : Fighter
{
    #region
    [SerializeField] private float attackRange;
    [SerializeField] private int attackedLayer;
    [SerializeField] private string targetTag = "Player";
    #endregion

    // Animation event
    public void Hit()
    {
        Vector2 attackDirection = Vector2.right * ((flip.isFacingRight) ? 1 : -1);
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y + 0.5f);
        RaycastHit2D[] hit = Physics2D.RaycastAll(rayOrigin, attackDirection, attackRange);

        foreach (var collider in hit)
        {
            if (collider.transform.CompareTag(targetTag))
            {
                var currentDamage = baseDamage;
                applyDamageModifiers?.Invoke(ref currentDamage);
                collider.transform.GetComponent<Health>().ApplyDamage(currentDamage);
                return;
            }
        }
    }
}
