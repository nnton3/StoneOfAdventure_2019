using UnityEngine;

public class IronPlate_Artifact : Artifact
{
    [SerializeField] private float damageResistance = 2f;

    public void AddGainPower()
    {
        var damageResist = player.AddComponent<DamageResistance>();
        damageResist.Initialize(damageResistance);
        AddArtifactOnCanvas();
        Destroy(gameObject);
    }
}
