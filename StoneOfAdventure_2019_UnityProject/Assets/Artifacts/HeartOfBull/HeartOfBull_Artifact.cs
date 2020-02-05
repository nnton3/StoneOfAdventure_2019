using StoneOfAdventure.Combat;
using UnityEngine;

public class HeartOfBull_Artifact : Artifact
{
    [SerializeField] [Range(0f, 1f)] private float addedHealthPoints;

    private Health playerHealth;

    protected override void Start()
    {
        base.Start();
        playerHealth = player.GetComponent<Health>();
    }

    public void AddHealthPoints()
    {
        playerHealth.UpdateMaxHealthPoints(addedHealthPoints);
        AddArtifactOnCanvas();
        Destroy(gameObject);
    }
}
