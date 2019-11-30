using UnityEngine;
using System.Collections.Generic;
using StoneOfAdventure.Combat;

public class RegenBuff : BaseBuff
{
    [SerializeField] private int healValue = 1;
    [SerializeField] private float periodicity = 1f;

    private Health health;

    public void Initialize(int _healValue, float _periodicity)
    {
        healValue = _healValue;
        periodicity = _periodicity;
    }

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    public override void ApplyBuff()
    {
        InvokeRepeating("Heal", 0f, periodicity);
    }

    public override void RemoveBuff()
    {
        CancelInvoke("Heal");
        Destroy(this);
    }

    private void Heal()
    {
        health.GetComponent<Health>().Heal(healValue);
    }
}
