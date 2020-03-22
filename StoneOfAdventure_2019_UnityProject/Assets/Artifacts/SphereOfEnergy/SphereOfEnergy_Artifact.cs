using UnityEngine;
using StoneOfAdventure.Combat;
using Zenject;

namespace StoneOfAdventure.Artifacts
{
    public class SphereOfEnergy_Artifact : Artifact
    {
        [SerializeField] private float targetTimeOnFeet = 2f;
        [SerializeField] private float bonusDamageInPercent = 1.5f;
        [SerializeField] private float bonusDamageInPercent_lvlCoef;

        public override void AddEffect()
        {
            base.AddEffect();
            if (artifactsController.GetArtLvl(this) == 1)
            {
                var ciriticalDamage = Container.InstantiateComponent<SphereOfEnergy_damageModifier>(player);
                ciriticalDamage.Initialize(targetTimeOnFeet, bonusDamageInPercent);
            }
            else
                player.GetComponent<SphereOfEnergy_damageModifier>().Initialize(
                    targetTimeOnFeet,
                    bonusDamageInPercent + artifactsController.GetArtLvl(this) * bonusDamageInPercent_lvlCoef);
        }
    }
}
