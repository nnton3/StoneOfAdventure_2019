using StoneOfAdventure.Combat;
using UnityEngine;

public class DamageResistance : MonoBehaviour
{
    private float damageResistance;

    public void Initialize(float damageResistance)
    {
        this.damageResistance = damageResistance;
    }

    private void Start()
    {
        GetComponent<Health>().AddModifierOfInputDamage(ApplyDamageResistance);
    }

    private void ApplyDamageResistance(ref int damage)
    {
        damage = (int)(damage - damage * damageResistance);
    }
}
