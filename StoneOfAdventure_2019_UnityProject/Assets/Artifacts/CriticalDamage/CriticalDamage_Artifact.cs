using UnityEngine;
using StoneOfAdventure.Combat;

namespace StoneOfAdventure.Artifacts
{
    public class CriticalDamage_Artifact : Artifact
    {
        [SerializeField] private float criticalChance = 15f;
        [SerializeField] private float addedDamageinPercent = 0.5f;
        [SerializeField] private float denominatorOfProgression;

        public override void AddEffect()
        {
            base.AddEffect();
            if (artifactsController.GetArtLvl(this) == 1)
            {
                var ciriticalDamage = Container.InstantiateComponent<CriticalDamage_damageModifier>(player.gameObject);
                ciriticalDamage.Initialize(addedDamageinPercent, criticalChance);
            }
            else
                player.GetComponent<CriticalDamage_damageModifier>().Initialize
                    (
                        addedDamageinPercent, 
                        CalculateNewChanceValue(criticalChance, denominatorOfProgression)
                    );
        }
    }
}
