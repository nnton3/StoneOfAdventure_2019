using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

namespace StoneOfAdventure.Core
{
    public class EnemieFactory : MonoBehaviour
    {
        private Tilemap groundTilemap;
        private GameObject player;

        [SerializeField] private TileBase targetTile;
        [SerializeField] private GameObject enemiePref;
        [SerializeField] private Vector3Int boundSize = new Vector3Int(1, 1, 1);
        [SerializeField] private float spawnDelay = 5f;

        private void Start()
        {
            player = FindObjectOfType<PlayerStateController>().gameObject;
            groundTilemap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();

            InvokeRepeating("SpawnEnemie", 1f, spawnDelay);
        }

        private void SpawnEnemie()
        {
            Vector3 spawnPosition = ChangeSpawnPosition();
            Instantiate(enemiePref, spawnPosition, Quaternion.identity);
            GameObject enemie = ChangeEnemie();

        }

        private Vector3 ChangeSpawnPosition()
        {
            List<Vector3> targetPositions = new List<Vector3>();

            Vector3 startCheckPoint = player.transform.position - (Vector3)boundSize / 2;

            for (int i = 1; i < boundSize.x; i++)
            {
                for (int j = 1; j < boundSize.y; j++)
                {
                    Vector3 worldPositionCheck = startCheckPoint + new Vector3(i, j, 0f);
                    Vector3Int tilePositionCheck = groundTilemap.WorldToCell(startCheckPoint + new Vector3(i, j, 0f));
                    if (groundTilemap.GetTile(tilePositionCheck) == targetTile)
                    {
                        Debug.Log(worldPositionCheck);
                        targetPositions.Add(worldPositionCheck);
                    }
                }
            }

            Vector3 positionForSpawn = targetPositions[UnityEngine.Random.Range(0, targetPositions.Count - 1)] + Vector3.up;
            Debug.Log($"Position for spawn = {positionForSpawn}");
            return positionForSpawn;
        }

        private GameObject ChangeEnemie()
        {
            return null;
        }
    }
}
