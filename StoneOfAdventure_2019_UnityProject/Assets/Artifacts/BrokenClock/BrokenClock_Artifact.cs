using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class BrokenClock_Artifact : Artifact
    {
        [SerializeField] [Range(0f, 1f)] private float chance = 0.2f;
        [SerializeField] private float reduceValue = 2.5f;

        public override void AddEffect()
        {
            base.AddEffect();
            var buff = player.AddComponent<BrokenClock_buff>();
            buff.Initialize(chance, reduceValue);
        }
    }
}
