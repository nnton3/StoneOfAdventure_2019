using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;
using System;

public class TrackingEye_HealthModifier : MonoBehaviour
{
    #region Variables
    private int attackNumberBlocked = 3;
    private int numerator;
    #endregion

    public void Initialize(int attackNumberBlocked)
    {
        this.attackNumberBlocked = attackNumberBlocked;
    }

    private void Start()
    {
        GetComponent<Health>().AddModifierOfInputDamage(TryToBlockNextAttack);
    }

    private void TryToBlockNextAttack(ref int damage)
    {
        numerator++;
        if (numerator == attackNumberBlocked)
        {
            damage = 0;
            numerator = 0;
        }
    }
}
