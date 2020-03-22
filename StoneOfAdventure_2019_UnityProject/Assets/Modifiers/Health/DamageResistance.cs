using StoneOfAdventure.Combat;
using UnityEngine;
using Zenject;

public class DamageResistance : MonoBehaviour
{
    private float damageResistanceInPercent;
    [Inject] private DiContainer Container;

    public void Initialize(float damageResistanceInPercent)
    {
        this.damageResistanceInPercent = damageResistanceInPercent;

        Container.Inject(this);
    }

    private void Start()
    {
        GetComponent<Health>().AddModifierOfInputDamage(ApplyDamageResistance);
    }

    private void ApplyDamageResistance(ref int damage)
    {
        damage = (int)(damage - damage * damageResistanceInPercent);
    }
}
