using StoneOfAdventure.Combat;
using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class HealthRegen_artifact : Artifact
    {
        [SerializeField] private int healValue;
        [SerializeField] private int addedHealPerArtLvl;
        Health Health;

        public override void AddEffect()
        {
            base.AddEffect();
            var healthRegen = player.GetComponent<HealthRegen>();
            healthRegen.HealValue = healValue + addedHealPerArtLvl * (artifactsController.GetArtLvl(this) - 1);
        }
    }
}
