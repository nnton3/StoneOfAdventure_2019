using UnityEngine;
using StoneOfAdventure.Combat;

namespace StoneOfAdventure.Artifacts
{
    public class SphereOfEnergy_Artifact : Artifact
    {
        [SerializeField] private float targetTimeOnFeet = 2f;
        [SerializeField] private float bonusDamageInPercent = 1.5f;

        public void AddGainPower()
        {
            var ciriticalDamage = player.AddComponent<SphereOfEnergy_damageModifier>();
            ciriticalDamage.Initialize(targetTimeOnFeet, bonusDamageInPercent);
            AddArtifactOnCanvas();
            Destroy(gameObject);
        }
    }
}
