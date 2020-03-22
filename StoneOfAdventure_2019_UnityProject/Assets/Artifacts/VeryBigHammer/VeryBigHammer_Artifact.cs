using UnityEngine;
using StoneOfAdventure.Combat;

namespace StoneOfAdventure.Artifacts
{
    public class VeryBigHammer_Artifact : Artifact
    {
        [SerializeField][Range(0f, 1f)] private float chance = 0.5f;
        [SerializeField] private float timeInStun = 2f;
        [SerializeField] private int damage = 20;
        [SerializeField] private float damage_lvlCoef;
        [SerializeField] private float denominatorOfProgression;

        public override void AddEffect()
        {
            base.AddEffect();
            if (artifactsController.GetArtLvl(this) == 1)
            {
                var buff = Container.InstantiateComponent<VeryBigHammer_attackModifier>(player);
                buff.Initialize(chance, timeInStun, damage);
            }
            else
                player.GetComponent<VeryBigHammer_attackModifier>().Initialize(
                    CalculateNewChanceValue(chance, denominatorOfProgression),
                    timeInStun,
                    (int)(damage + artifactsController.GetArtLvl(this) * damage_lvlCoef));

        }
    }
}
