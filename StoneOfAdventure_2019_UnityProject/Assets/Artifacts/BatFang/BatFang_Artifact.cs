using UnityEngine;
using StoneOfAdventure.Combat;
using Zenject;

namespace StoneOfAdventure.Artifacts
{
    public class BatFang_Artifact : Artifact
    {
        [SerializeField] private float lifestealInPercent = 0.02f;
        [Inject] DiContainer Container;

        public override void AddEffect()
        {
            base.AddEffect();
            artifactsController.AddArt(this);
            if (artifactsController.GetArtLvl(this) == 1)
            {
                var lifesteal = Container.InstantiateComponent<BatFang_attackEffect>(player);
                lifesteal.Initialize(lifestealInPercent);
            }
            else
            {
                var newLifestealValue = lifestealInPercent + (artifactsController.GetArtLvl(this) - 1) * 0.01f;
                player.GetComponent<BatFang_attackEffect>().Initialize(newLifestealValue);
            }
        }
    }
}
