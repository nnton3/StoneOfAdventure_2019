using StoneOfAdventure.Combat;
using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class SaveEffortBoots_Artifact : Artifact
    {
        [SerializeField] private float buffTime = 10f;
        [SerializeField] private float timeToApply = 5f;
        [SerializeField] [Range(0f, 1f)] private float movespeedGain = 1f;
        [SerializeField] private float movespeedGainCoef = 0.5f;

        public override void AddEffect()
        {
            base.AddEffect();
            if (artifactsController.GetArtLvl(this) == 1)
            {
                var buff = Container.InstantiateComponent<SaveEffortBoots_buff>(player.gameObject);
                buff.Initialize(buffTime, timeToApply, movespeedGain);
            }
            else
                player.GetComponent<SaveEffortBoots_buff>().Initialize(
                    buffTime, 
                    timeToApply, 
                    movespeedGain + artifactsController.GetArtLvl(this) * movespeedGainCoef);
        }
    }
}
