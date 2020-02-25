using UnityEngine;
using StoneOfAdventure.Combat;

namespace StoneOfAdventure.Artifacts
{
    public class SphereOfEnergy_Artifact : Artifact
    {
        [SerializeField] private float targetTimeOnFeet = 2f;
        [SerializeField] private float bonusDamageInPercent = 1.5f;

        public override void AddEffect()
        {
            base.AddEffect();
            var ciriticalDamage = player.AddComponent<SphereOfEnergy_damageModifier>();
            ciriticalDamage.Initialize(targetTimeOnFeet, bonusDamageInPercent);
        }
    }
}
