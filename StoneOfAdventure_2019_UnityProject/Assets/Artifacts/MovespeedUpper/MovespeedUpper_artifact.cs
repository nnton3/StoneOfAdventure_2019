using UnityEngine;
using StoneOfAdventure.Movement;
using Zenject;

namespace StoneOfAdventure.Artifacts
{
    public class MovespeedUpper_artifact : Artifact
    {
        [SerializeField] private float addedMovespeedInPercent;
        [Inject(Id = "Player")] private Mover playerMover;

        public override void AddEffect()
        {
            base.AddEffect();
            playerMover.ModifyBaseMovespeed(playerMover.BaseMovespeed + playerMover.BaseMovespeed * addedMovespeedInPercent);
        }
    }
}
