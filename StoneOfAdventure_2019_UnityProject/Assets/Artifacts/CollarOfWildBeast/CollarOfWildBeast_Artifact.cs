using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class CollarOfWildBeast_Artifact : Artifact
    {
        [SerializeField] [Range(0f, 1f)] private float newDodgeChance;

        public override void AddEffect()
        {
            var dodgeChance = player.AddComponent<DodgeChance>();
            dodgeChance.Initialize(newDodgeChance);
        }
    }
}
