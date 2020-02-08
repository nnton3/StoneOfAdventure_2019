using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class CollarOfWildBeast_Artifact : Artifact
    {
        [SerializeField] [Range(0f, 1f)] private float newDodgeChance;

        public void AddDodgeChance()
        {
            var dodgeChance = player.AddComponent<DodgeChance>();
            dodgeChance.Initialize(newDodgeChance);
            AddArtifactOnCanvas();
            Destroy(gameObject);
        }
    }
}
