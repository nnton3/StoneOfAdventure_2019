using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonBlood_Artifact : Artifact
{
    [SerializeField] private int maxStucsValue = 3;
    [SerializeField] private float effectTime = 3f;
    [SerializeField] private float healPerSecPerStuc = 4f;

    public void AddDemonBlood()
    {
        DemonBlood_HealthModifier demonBlood = player.AddComponent<DemonBlood_HealthModifier>();
        demonBlood.Initialize(maxStucsValue, effectTime, healPerSecPerStuc);
        AddArtifactOnCanvas();
        Destroy(gameObject);
    }
}
