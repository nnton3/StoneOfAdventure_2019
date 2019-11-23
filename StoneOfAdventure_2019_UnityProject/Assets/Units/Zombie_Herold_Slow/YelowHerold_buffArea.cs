using StoneOfAdventure.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YelowHerold_buffArea : BuffArea
{
    [SerializeField] private float slowInPercent = 0.5f;
    [SerializeField] private float actionTime = 3f;

    private Dictionary<GameObject, SlowDown_effect> buffedUnits = new Dictionary<GameObject, SlowDown_effect>();

    protected override void AddBuffs(Collider2D collision)
    {
        var target = collision.gameObject;
        buffedUnits.Add(target, null);
        ApplySlowDownAttack(target);
    }

    private void ApplySlowDownAttack(GameObject target)
    {
        if (!target.GetComponent<Fighter>()) return;

        var slowDownAttack = target.AddComponent<SlowDown_effect>();
        buffedUnits[target] = slowDownAttack;
        slowDownAttack.Initialize(slowInPercent, actionTime);
    }

    protected override void RemoveBuffs(Collider2D collision)
    {
        buffedUnits[collision.gameObject].RemoveBuff();
        buffedUnits.Remove(collision.gameObject);
    }
}
