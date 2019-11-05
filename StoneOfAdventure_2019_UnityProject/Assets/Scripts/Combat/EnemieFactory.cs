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
        [SerializeField] private Vector3Int boundSize = new Vector3Int(1, 1, 1);
        [SerializeField] private float spawnDelay = 5f;

        [SerializeField] private List<GameObject> units = new List<GameObject>();
        [SerializeField] private List<float> spawnChance = new List<float>();

        private void Start()
        {
            player = FindObjectOfType<PlayerStateController>().gameObject;
            groundTilemap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();

            InvokeRepeating("SpawnEnemie", 1f, spawnDelay);
        }

        private void SpawnEnemie()
        {
            Vector3 spawnPosition = ChangeSpawnPosition();
            if (spawnPosition == Vector3.zero) return;
            GameObject enemie = ChangeEnemie();
            Instantiate(enemie, spawnPosition, Quaternion.identity);
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
                    Vector3Int tilePositionCheck = groundTilemap.WorldToCell(worldPositionCheck);
                    if (groundTilemap.GetTile(tilePositionCheck) == targetTile)
                    {
                        targetPositions.Add(tilePositionCheck);
                    }
                }
            }

            if (targetPositions.Count == 0) return Vector3.zero;

            Vector3 positionForSpawn = targetPositions[UnityEngine.Random.Range(0, targetPositions.Count - 1)] + Vector3.up;
            Vector2 positionForSpawn2d = positionForSpawn;
            return positionForSpawn2d;
        }

        private GameObject ChangeEnemie()
        {
            float chance = UnityEngine.Random.Range(0f, 100f);
            float currentSpawnRange = 0f;

            for (int i = 0; i < units.Count; i++)
            {
                currentSpawnRange += spawnChance[i];
                if (chance <= currentSpawnRange)
                {
                    return units[i];
                }
            }
            Debug.LogError("not found enemie pref");
            return null;
        }
    }
}
