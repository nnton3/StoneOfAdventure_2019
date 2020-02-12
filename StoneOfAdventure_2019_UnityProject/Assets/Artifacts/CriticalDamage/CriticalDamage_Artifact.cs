using UnityEngine;
using StoneOfAdventure.Combat;

namespace StoneOfAdventure.Artifacts
{
    public class CriticalDamage_Artifact : Artifact
    {
        [SerializeField] private float criticalChance = 50f;
        [SerializeField] private float damageScale = 1.5f;

        public override void AddEffect()
        {
            var ciriticalDamage = player.AddComponent<CriticalDamage_damageModifier>();
            ciriticalDamage.Initialize(damageScale, criticalChance);
        }
    }
}
