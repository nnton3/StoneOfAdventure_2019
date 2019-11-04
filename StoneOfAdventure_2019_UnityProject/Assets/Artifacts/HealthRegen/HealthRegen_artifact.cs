using StoneOfAdventure.Combat;
using UnityEngine;

public class HealthRegen_artifact : Artifact
{
    [SerializeField] private float periodicity;
    [SerializeField] private float healValue;
    Health Health;

    public void AddRegen()
    {
        HealthRegen healthRegen = player.GetComponent<HealthRegen>();
        healthRegen.Periodicity = periodicity;
        healthRegen.HealValue = healValue;
        Destroy(gameObject);
    }
}
