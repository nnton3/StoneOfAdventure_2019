using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class RingOfDetermination_Artifact : Artifact
    {
        [SerializeField] private int sixtyPercentHeal = 5;
        [SerializeField] private int fourtyPercentHeal = 8;
        [SerializeField] private int twetyPercentHeal = 10;

        public override void AddEffect()
        {
            base.AddEffect();
            var buff = player.AddComponent<RingOfDetermination_buff>();
            buff.Initialize(sixtyPercentHeal, fourtyPercentHeal, twetyPercentHeal);
        }
    }
}
