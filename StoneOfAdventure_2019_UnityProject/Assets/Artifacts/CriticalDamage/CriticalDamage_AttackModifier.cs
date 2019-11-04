using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;

public class CriticalDamage_AttackModifier : MonoBehaviour
{
    private PlayerFighter playerFighter;
    public float DamageScale = 1.5f;
    public float CriticalChance = 50f;

    private void Start()
    {
        playerFighter = GetComponent<PlayerFighter>();

        playerFighter.Attack.AddListener(CalculateDamageScale);
    }

    private void CalculateDamageScale()
    {
        float chance = Random.Range(0f, 100f);
        if (chance <= CriticalChance)
        {
            Debug.Log("work");
            playerFighter.SetDamageScaleForNexAttack(DamageScale);
        }
    }
}
