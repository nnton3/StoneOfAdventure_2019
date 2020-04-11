using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class TrackingEye_Artifact : Artifact
    {
        [SerializeField] private float timeToBlock = 9;
        [SerializeField] private float timeToBlock_lvlCoef;

        public override void AddEffect()
        {
            base.AddEffect();
            if (artifactsController.GetArtLvl(this) == 1)
            {
                var ciriticalDamage = Container.InstantiateComponent<TrackingEye_HealthModifier>(player.gameObject);
                ciriticalDamage.Initialize(timeToBlock);
            }
            else
                player.GetComponent<TrackingEye_HealthModifier>().Initialize(timeToBlock - artifactsController.GetArtLvl(this) * timeToBlock_lvlCoef);
        }
    }
}
