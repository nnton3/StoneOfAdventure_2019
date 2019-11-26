﻿using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

namespace StoneOfAdventure.Core
{
    public class EnemieFactory : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float baseSpawnDelay = 5f;
        [SerializeField] private float minSpawnDelay = 1f;
        private GroundTileFinder tileFinder;
        [SerializeField] private int totalTickNumber = 40;
        private int currentTickNumber = 0;
        private float spawnDelayStep = 0f;

        [SerializeField] private List<GameObject> units = new List<GameObject>();
        [SerializeField] private List<float> baseSpawnChance = new List<float>();
        [SerializeField] private List<float> endSpawnChance = new List<float>();
        private float spawnChanceIncreaseStep = 0f;
        #endregion

        private void Start()
        {
            tileFinder = GetComponent<GroundTileFinder>();
            StartCoroutine("SpawnEmmiter");
            spawnDelayStep = (baseSpawnDelay - minSpawnDelay) / totalTickNumber; 
        }

        private IEnumerator SpawnEmmiter()
        {
            yield return new WaitForSeconds(CalculateSpawnDelay());
            SpawnEnemie();
            currentTickNumber++;
            StartCoroutine("SpawnEmmiter");
        } 

        private float CalculateSpawnDelay()
        {
            var currentSpawnDelay = baseSpawnDelay - currentTickNumber * spawnDelayStep;
            if (currentTickNumber == totalTickNumber) return minSpawnDelay;
            Debug.Log($"Текущая длительность тика равна = {currentSpawnDelay}");
            return currentSpawnDelay;
        }

        private void SpawnEnemie()
        {
            for (int i = 0; i < units.Count; i++)
            {
                float chance = Random.Range(0f, 100f);
                var currentChance = 0f;

                if (currentTickNumber == totalTickNumber) currentChance = endSpawnChance[i];
                else
                {
                spawnChanceIncreaseStep = (endSpawnChance[i] - baseSpawnChance[i]) / totalTickNumber;
                currentChance = baseSpawnChance[i] + 
                     currentTickNumber * spawnChanceIncreaseStep;
                }
                Debug.Log($"Текущий шанс появления = {currentChance}");
                if (chance <= currentChance)
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
