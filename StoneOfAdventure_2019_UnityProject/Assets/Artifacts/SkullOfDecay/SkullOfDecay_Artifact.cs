using UnityEngine;
using StoneOfAdventure.Combat;
namespace StoneOfAdventure.Artifacts
{
    public class SkullOfDecay_Artifact : Artifact
    {
        [SerializeField] [Range(0f, 1f)] private float damageInPercent = 0.015f;
        [SerializeField] [Range(0f, 1f)] private float lifesteal = 0.25f;
        [SerializeField] private float periodicity = 1f;

        public override void AddEffect()
        {
            var ciriticalDamage = player.AddComponent<SkullOfDecay_buff>();
            ciriticalDamage.Initialize(damageInPercent, lifesteal, periodicity);
        }
    }
}
