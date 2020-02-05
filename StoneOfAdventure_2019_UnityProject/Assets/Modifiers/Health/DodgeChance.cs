using StoneOfAdventure.Combat;
using UnityEngine;

public class DodgeChance : MonoBehaviour
{
    private float dodgeChance;

    public void Initialize(float dodgeChance)
    {
        this.dodgeChance = dodgeChance;
    }

    private void Start()
    {
        GetComponent<Health>().AddModifierOfInputDamage(TryToDodge);
    }

    private void TryToDodge(ref int damage)
    {
        var chance = Random.Range(0f, 1f);
        if (chance < dodgeChance) damage = 0; 
    }
}
