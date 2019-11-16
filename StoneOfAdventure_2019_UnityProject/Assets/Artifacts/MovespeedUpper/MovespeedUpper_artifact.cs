using UnityEngine;
using StoneOfAdventure.Movement;

public class MovespeedUpper_artifact : Artifact
{
    [SerializeField][Range(0f, 1f)] private float addedMovespeedInPercent;

    public void AddMovespeed()
    {
        player.GetComponent<Mover>().ModifyMovespeed(addedMovespeedInPercent);
        Destroy(gameObject);
    }
}
