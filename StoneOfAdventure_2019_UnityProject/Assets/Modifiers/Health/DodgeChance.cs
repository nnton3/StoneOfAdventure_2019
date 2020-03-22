using StoneOfAdventure.Combat;
using UnityEngine;
using Zenject;

public class DodgeChance : MonoBehaviour
{
    private float dodgeChance;
    [Inject] private DiContainer Container;

    public void Initialize(float dodgeChance)
    {
        this.dodgeChance = dodgeChance;

        Container.Inject(this);
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
