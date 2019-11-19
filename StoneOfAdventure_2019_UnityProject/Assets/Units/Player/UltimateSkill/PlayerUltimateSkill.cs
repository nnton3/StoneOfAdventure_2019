using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class PlayerUltimateSkill : SkillBase
{
    #region Variables
    [SerializeField] private GameObject playerIllusionPref;
    private GroundTileFinder tileFinder; 
    [SerializeField] private readonly int IllusionsNumber = 3;
    #endregion

    private void Start()
    {
        tileFinder = GetComponent<GroundTileFinder>();
    }

    public override void StartUse()
    {
        base.StartUse();
        InstanceIllusions();
    }

    private void InstanceIllusions()
    {
        var validTiles = tileFinder.FindValidPositions();

        for (int i = 0; i < IllusionsNumber; i++)
        {
            if (validTiles.Count == 0) return;
            var instancePosition = validTiles[Random.Range(0, validTiles.Count - 1)] + Vector3.up;
            validTiles.Remove(instancePosition);
            Instantiate(playerIllusionPref, instancePosition, Quaternion.identity);
        }
    }
}
