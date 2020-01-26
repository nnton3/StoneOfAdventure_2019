using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace StoneOfAdventure.Core
{
    public class EnemieFactory : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float baseSpawnDelay = 5f;
        [SerializeField] private float minSpawnDelay = 1f;
        [SerializeField] private List<GameObject> units = new List<GameObject>();
        [SerializeField] private List<float> baseSpawnChance = new List<float>();
        [SerializeField] private List<float> endSpawnChance = new List<float>();
        [SerializeField] private int totalTickNumber = 40;

        private GroundTileFinder tileFinder;
        private int currentTickNumber = 0;
        private float spawnDelayStep = 0f;
        private float spawnChanceIncreaseStep = 0f;
        private LocationPointsStorage pointsStorage;
        #endregion

        private void Start()
        {
            tileFinder = GetComponent<GroundTileFinder>();
            pointsStorage = FindObjectOfType<LocationPointsStorage>();

            pointsStorage.LocationCompleted.AddListener(StopSpawn);

            spawnDelayStep = (baseSpawnDelay - minSpawnDelay) / totalTickNumber;

            StartCoroutine("SpawnEmmiter");
        }

        public void StopSpawn()
        {
            StopCoroutine("SpawnEmmiter");
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
            if (currentTickNumber >= totalTickNumber) return minSpawnDelay;
            var currentSpawnDelay = baseSpawnDelay - currentTickNumber * spawnDelayStep;
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
