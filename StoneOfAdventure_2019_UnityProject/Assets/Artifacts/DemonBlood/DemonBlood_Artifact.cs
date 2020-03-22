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
            var demonBlood = Container.InstantiateComponent<DemonBlood_HealthModifier>(player);
            var healPerStuckValue = healPerSecPerStuc + artifactsController.GetArtLvl(this) - 1;
            demonBlood.Initialize(maxStucsValue, effectTime, healPerSecPerStuc);
        }
    }
}
