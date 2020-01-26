﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFang_Artifact : Artifact
{
    [SerializeField] private float lifestealInPercent = 0.02f;

    public void AddBatFang()
    {
        var lifesteal = player.AddComponent<BatFang_attackEffect>();
        lifesteal.Initialize(lifestealInPercent);
        AddArtifactOnCanvas();
        Destroy(gameObject);
    }
}
