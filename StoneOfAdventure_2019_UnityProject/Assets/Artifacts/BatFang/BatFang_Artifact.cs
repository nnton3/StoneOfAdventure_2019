using UnityEngine;
using StoneOfAdventure.Combat;

namespace StoneOfAdventure.Artifacts
{
    public class BatFang_Artifact : Artifact
    {
        [SerializeField] private float lifestealInPercent = 0.02f;

        public override void AddEffect()
        {
            var lifesteal = player.AddComponent<BatFang_attackEffect>();
            lifesteal.Initialize(lifestealInPercent);
        }
    }
}
