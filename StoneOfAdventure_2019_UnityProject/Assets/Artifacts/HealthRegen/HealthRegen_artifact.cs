using StoneOfAdventure.Combat;
using UnityEngine;

public class HealthRegen_artifact : Artifact
{
    [SerializeField] private float healValue;
    Health Health;

    public void AddRegen()
    {
        HealthRegen healthRegen = player.GetComponent<HealthRegen>();
        healthRegen.HealValue = healValue;
        AddArtifactOnCanvas();
        Destroy(gameObject);
    }
}
