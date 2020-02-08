using StoneOfAdventure.Combat;
using UnityEngine;

public class BlockChance : MonoBehaviour
{
    private float blockChance;

    public void Initialize(float blockChance)
    {
        this.blockChance = blockChance;
    }

    private void Start()
    {
        GetComponent<Health>().AddModifierOfInputDamage(TryToBlock);
    }

    private void TryToBlock(ref int damage)
    {
        var chance = Random.Range(0f, 1f);
        if (chance < blockChance) damage = 0;
    }
}
