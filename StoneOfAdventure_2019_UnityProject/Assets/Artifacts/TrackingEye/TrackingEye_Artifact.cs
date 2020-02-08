using System.Collections;
using StoneOfAdventure.Combat;
using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class TrackingEye_Artifact : Artifact
    {
        [SerializeField] private int attackBlockedNumber = 9;

        public void AddTrackingEye()
        {
            var ciriticalDamage = player.AddComponent<TrackingEye_HealthModifier>();
            ciriticalDamage.Initialize(attackBlockedNumber);
            AddArtifactOnCanvas();
            Destroy(gameObject);
        }
    }
}
