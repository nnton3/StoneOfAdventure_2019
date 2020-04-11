using UnityEngine;
using Zenject;

namespace StoneOfAdventure.Artifacts
{
    public class BrokenClock_Artifact : Artifact
    {
        [SerializeField] [Range(0f, 1f)] private float chance = 0.2f;
        [SerializeField] private float reduceValue = 2.5f;
        [SerializeField] private float denominatorOfProgression;

        public override void AddEffect()
        {
            base.AddEffect();
            if (artifactsController.GetArtLvl(this) == 1)
            {
                var buff = Container.InstantiateComponent<BrokenClock_buff>(player.gameObject);
                buff.Initialize(chance, reduceValue);
            }
            else
                player.GetComponent<BrokenClock_buff>().Initialize(CalculateNewChanceValue(chance, denominatorOfProgression), reduceValue);
        }
    }
}
