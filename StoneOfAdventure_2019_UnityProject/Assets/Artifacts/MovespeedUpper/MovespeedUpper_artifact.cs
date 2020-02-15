using UnityEngine;
using StoneOfAdventure.Movement;

namespace StoneOfAdventure.Artifacts
{
    public class MovespeedUpper_artifact : Artifact
    {
        [SerializeField] private float addedMovespeedInPercent;

        public override void AddEffect()
        {
            var playerMover = player.GetComponent<Mover>();
            playerMover.ModifyBaseMovespeed(playerMover.BaseMovespeed * addedMovespeedInPercent);
        }
    }
}
