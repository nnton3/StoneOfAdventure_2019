using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class DemonBlood_Artifact : Artifact
    {
        [SerializeField] private int maxStucsValue = 3;
        [SerializeField] private float effectTime = 3f;
        [SerializeField] private float healPerSecPerStuc = 4f;

        public override void AddEffect()
        {
            base.AddEffect();
            var demonBlood = player.AddComponent<DemonBlood_HealthModifier>();
            demonBlood.Initialize(maxStucsValue, effectTime, healPerSecPerStuc);
        }
    }
}
