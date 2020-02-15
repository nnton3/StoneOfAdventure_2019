using StoneOfAdventure.Combat;
using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class HealthRegen_artifact : Artifact
    {
        [SerializeField] private int healValue;
        Health Health;

        public override void AddEffect()
        {
            var healthRegen = player.GetComponent<HealthRegen>();
            healthRegen.HealValue = healValue;
        }
    }
}
