using UnityEngine;
using StoneOfAdventure.Combat;

namespace StoneOfAdventure.Artifacts
{
    public class VeryBigHammer_Artifact : Artifact
    {
        [SerializeField][Range(0f, 1f)] private float chance = 0.5f;
        [SerializeField] private float timeInStun = 2f;
        [SerializeField] private int damage = 20;

        public void AddVeryBigHammer()
        {
            var buff = player.AddComponent<VeryBigHammer_attackModifier>();
            buff.Initialize(chance, timeInStun, damage);
            AddArtifactOnCanvas();
            Destroy(gameObject);
        }
    }
}
