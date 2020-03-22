using UnityEngine;
using Zenject;

namespace StoneOfAdventure.Artifacts
{
    public class CollarOfWildBeast_Artifact : Artifact
    {
        [SerializeField] [Range(0f, 1f)] private float dodgeChance;
        [SerializeField] private float denominatorOfProgression;

        public override void AddEffect()
        {
            base.AddEffect();
            if (artifactsController.GetArtLvl(this) == 1)
            {
                var dodgeChance = Container.InstantiateComponent<DodgeChance>(player);
                dodgeChance.Initialize(this.dodgeChance);
            }
            else
                player.GetComponent<DodgeChance>().Initialize(CalculateNewChanceValue(dodgeChance, denominatorOfProgression));
        }
    }
}
