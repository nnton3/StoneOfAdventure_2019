using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class IronPlate_Artifact : Artifact
    {
        [SerializeField][Range(0f, 1f)] private float damageResistanceInPercent = 0.02f;
        [SerializeField] private float denominatorOfProgression;

        public override void AddEffect()
        {
            base.AddEffect();
            if (artifactsController.GetArtLvl(this) == 1)
            {
                var damageResist = Container.InstantiateComponent<DamageResistance>(player);
                damageResist.Initialize(damageResistanceInPercent);
            }
            else
                player.GetComponent<DamageResistance>().Initialize(CalculateNewChanceValue(damageResistanceInPercent, denominatorOfProgression));
        }
    }
}
