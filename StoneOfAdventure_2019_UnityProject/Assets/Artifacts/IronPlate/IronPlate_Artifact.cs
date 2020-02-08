﻿using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class IronPlate_Artifact : Artifact
    {
        [SerializeField] private float damageResistance = 2f;

        public void AddIronPlate()
        {
            var damageResist = player.AddComponent<DamageResistance>();
            damageResist.Initialize(damageResistance);
            AddArtifactOnCanvas();
            Destroy(gameObject);
        }
    }
}
