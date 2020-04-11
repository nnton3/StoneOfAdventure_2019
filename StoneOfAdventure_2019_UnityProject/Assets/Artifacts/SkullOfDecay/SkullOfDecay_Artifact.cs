using UnityEngine;
using StoneOfAdventure.Combat;
namespace StoneOfAdventure.Artifacts
{
    public class SkullOfDecay_Artifact : Artifact
    {
        [SerializeField] [Range(0f, 1f)] private float damageInPercent = 0.015f;
        [SerializeField] [Range(0f, 1f)] private float lifesteal = 0.25f;
        [SerializeField] private float periodicity = 1f;
        [SerializeField] private float damageInPercentCoef;

        public override void AddEffect()
        {
            base.AddEffect();
            if (artifactsController.GetArtLvl(this) == 1)
            {
                var ciriticalDamage = Container.InstantiateComponent<SkullOfDecay_buff>(player.gameObject);
                ciriticalDamage.Initialize(damageInPercent, lifesteal, periodicity);
            }
            else
                player.GetComponent<SkullOfDecay_buff>().Initialize(
                    damageInPercent + artifactsController.GetArtLvl(this) * damageInPercentCoef,
                    lifesteal,
                    periodicity);
        }
    }
}
