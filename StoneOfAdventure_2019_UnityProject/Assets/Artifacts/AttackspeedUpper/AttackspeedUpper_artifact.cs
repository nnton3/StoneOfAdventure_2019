using UnityEngine;
using StoneOfAdventure.Combat;
using Zenject;

namespace StoneOfAdventure.Artifacts
{
    public class AttackspeedUpper_artifact : Artifact
    {
        [SerializeField] private float addedAttackSpeed;

        [Inject(Id = "Player")] private Fighter playerFighter;

        public override void AddEffect()
        {
            base.AddEffect();
            artifactsController.AddArt(this);
            playerFighter.ModifyAttackSpeed(addedAttackSpeed);
        }
    }
}
