using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

namespace StoneOfAdventure.Core
{
    public class EnemieFactory : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float baseSpawnDelay = 5f;
        [SerializeField] private float baseComplexity = 1f;
        private GroundTileFinder tileFinder;
        private float startTime = 0f;

        [SerializeField] private List<GameObject> units = new List<GameObject>();
        [SerializeField] private List<float> spawnChance = new List<float>();
        #endregion

        private void Start()
        {
            tileFinder = GetComponent<GroundTileFinder>();
            startTime = Time.time;
            InvokeRepeating("SpawnEnemie", baseSpawnDelay, CalculateSpawnDelay());
        }

        private float CalculateSpawnDelay()
        {
            var currentSpawnDelay = baseSpawnDelay - baseComplexity * (Time.time - startTime);
            if (currentSpawnDelay < 1f) return 1f;
            return currentSpawnDelay;
        }

        private void SpawnEnemie()
        {
            for (int i = 0; i < units.Count; i++)
            {
                float chance = UnityEngine.Random.Range(0f, 100f);

                if (chance <= spawnChance[i])
                {
                    var spawnPosition = ChangeSpawnPosition();
                    if (spawnPosition == Vector3.zero) return;
                    Instantiate(units[i], spawnPosition, Quaternion.identity);
                }
            }
        }

        private Vector3 ChangeSpawnPosition()
        {
            List<Vector3> targetPositions = tileFinder.FindValidPositions();

            if (targetPositions.Count == 0) return Vector3.zero;

            Vector3 positionForSpawn = targetPositions[UnityEngine.Random.Range(0, targetPositions.Count - 1)] + Vector3.up;
            Vector2 positionForSpawn2d = positionForSpawn;
            return positionForSpawn2d;
        }
    }
}
