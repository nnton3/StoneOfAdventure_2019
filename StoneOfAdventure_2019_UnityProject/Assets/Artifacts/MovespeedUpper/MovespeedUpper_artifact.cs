using UnityEngine;
using StoneOfAdventure.Movement;

public class MovespeedUpper_artifact : Artifact
{
    [SerializeField] private float addedMovespeedInPercent;

    public void AddMovespeed()
    {
        var playerMover = player.GetComponent<Mover>();
        playerMover.ModifyBaseMovespeed(playerMover.BaseMovespeed * addedMovespeedInPercent);
        AddArtifactOnCanvas();
        Destroy(gameObject);
    }
}
