using UnityEngine;
using System.Collections;

public class MovespeedUpper_artifact : Artifact
{
    [SerializeField][Range(0f, 1f)] private float addedMovespeedInPercent;

    public void AddMovespeed()
    {
        player.GetComponent<PlayerStateController>().MovespeedScale += addedMovespeedInPercent;
        Destroy(gameObject);
    }
}
