using StoneOfAdventure.Combat;
using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class SaveEffortBoots_Artifact : Artifact
    {
        [SerializeField] private float buffTime = 10f;
        [SerializeField] private float timeToApply = 5f;
        [SerializeField] [Range(0f, 1f)] private float movespeedGain = 1f;

        public void AddSaveEffortBoots()
        {
            var buff = player.AddComponent<SaveEffortBoots_buff>();
            buff.Initialize(buffTime, timeToApply, movespeedGain);
            AddArtifactOnCanvas();
            Destroy(gameObject);
        }
    }
}
