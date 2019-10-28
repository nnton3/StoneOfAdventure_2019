using StoneOfAdventure.Core;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemieFactory : MonoBehaviour
{
    private Tilemap groundTilemap;
    private Collider2D targetCol;

    private void Start()
    {
        groundTilemap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();
    }

    private void SpawnEnemie()
    {
        Vector3 spawnPosition = ChangeSpawnPosition();
        GameObject enemie = ChangeEnemie();

    }

    private Vector3 ChangeSpawnPosition()
    {
        BoundsInt newB = new BoundsInt();
        
        TileBase[] groundTiles = groundTilemap.GetTilesBlock(newB);

        return Vector3.zero;
    }

    private GameObject ChangeEnemie()
    {
        throw new NotImplementedException();
    }
}
