using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class RingOfDetermination_Artifact : Artifact
    {
        [SerializeField] private int sixtyPercentHeal = 5;
        [SerializeField] private int fourtyPercentHeal = 8;
        [SerializeField] private int twentyPercentHeal = 10;

        public override void AddEffect()
        {
            base.AddEffect();
            var currentArtLvl = artifactsController.GetArtLvl(this);
            if (currentArtLvl == 1)
            {
                var buff = Container.InstantiateComponent<RingOfDetermination_buff>(player);
                buff.Initialize(sixtyPercentHeal, fourtyPercentHeal, twentyPercentHeal);
            }
            else
                player.GetComponent<RingOfDetermination_buff>().Initialize(
                    sixtyPercentHeal + currentArtLvl,
                    fourtyPercentHeal + currentArtLvl * 2,
                    twentyPercentHeal + currentArtLvl * 3);
        }
    }
}
