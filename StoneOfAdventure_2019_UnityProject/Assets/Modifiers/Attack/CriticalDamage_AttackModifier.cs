using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;

public class CriticalDamage_AttackModifier : MonoBehaviour
{
    private PlayerFighter playerFighter;
    private float damageScale = 1.5f;
    private float criticalChance = 50f;

    public void Initialize(float _damageScale, float _criticalChance)
    {
        damageScale = _damageScale;
        criticalChance = _criticalChance;
    }

    private void Start()
    {
        playerFighter = GetComponent<PlayerFighter>();

        playerFighter.Attack.AddListener(CalculateDamageScale);
    }

    private void CalculateDamageScale()
    {
        float chance = Random.Range(0f, 100f);
        if (chance <= criticalChance)
        {
            playerFighter.SetDamageScaleForNexAttack(damageScale);
        }
    }
}
