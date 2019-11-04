using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;

public class AttackspeedUpper_artifact : Artifact
{
    [SerializeField][Range(0f, 1f)] private float addedAttackspeedInPercent;

    private PlayerFighter playerFighter;

    protected override void Start()
    {
        base.Start();
        playerFighter = player.GetComponent<PlayerFighter>();
    }

    public void AddAttackspeed()
    {
        playerFighter.ModifyAttackSpeed(addedAttackspeedInPercent);
        Destroy(gameObject);
    }
}
