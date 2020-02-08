﻿using StoneOfAdventure.Combat;
using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class HealthRegen_artifact : Artifact
    {
        [SerializeField] private int healValue;
        Health Health;

        public void AddRegen()
        {
            HealthRegen healthRegen = player.GetComponent<HealthRegen>();
            healthRegen.HealValue = healValue;
            AddArtifactOnCanvas();
            Destroy(gameObject);
        }
    }
}
