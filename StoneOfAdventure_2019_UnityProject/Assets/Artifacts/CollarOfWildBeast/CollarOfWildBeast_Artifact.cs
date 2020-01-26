using System.Collections;
using StoneOfAdventure.Combat;
using UnityEngine;

public class CollarOfWildBeast_Artifact : Artifact
{
    [SerializeField] [Range(0f, 1f)] private float newDodgeChance;

    private Health playerHealth;

    protected override void Start()
    {
        base.Start();
        playerHealth = player.GetComponent<Health>();
    }

    public void AddDodgeChance()
    {
        playerHealth.DodgeChance = newDodgeChance;
        AddArtifactOnCanvas();
        Destroy(gameObject);
    }
}
