using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;
using System;

public class TrackingEye_HealthModifier : MonoBehaviour
{
    #region Variables
    private int attackNumberBlocked = 3;
    private Health health;
    private int numerator;
    #endregion
    public void Initialize(int attackNumberBlocked)
    {
        this.attackNumberBlocked = attackNumberBlocked;
    }

    private void Start()
    {
        health = GetComponent<Health>();

        health.HPDecreased.AddListener(TryToBlockNextAttack);
    }

    private void TryToBlockNextAttack()
    {
        numerator++;
        if (numerator == attackNumberBlocked)
        {
            health.BlockNextDamage();
            numerator = 0;
        }
    }
}
