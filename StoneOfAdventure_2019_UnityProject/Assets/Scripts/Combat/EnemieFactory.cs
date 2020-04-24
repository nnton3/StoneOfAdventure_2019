using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;
using Zenject;

namespace StoneOfAdventure.Core
{
    public class EnemieFactory : MonoBehaviour
    {
        #region Variables
        [Inject] private EnemieSpawnerConfig spawnerConfig;
        [Inject] private GroundTileFinder tileFinder;
        [Inject] readonly SignalBus signalBus;
        [Inject (Id = "Player")] private PlayerStateController player;
        [Inject] private EnemiePool enemiePool;

        private int currentTickNumber = 0;
        private float spawnDelayStep = 0f;
        private float spawnChanceIncreaseStep = 0f;
        #endregion

        private void Start()
        {
            signalBus.Subscribe<LocationCompletedSignal>(StopSpawn);
            spawnDelayStep = (spawnerConfig.BaseSpawnDelay - spawnerConfig.MinSpawnDelay) / spawnerConfig.TotalTickNumber;
            StartCoroutine("SpawnEmmiter");
        }

        public void StopSpawn() => StopCoroutine("SpawnEmmiter");

        private IEnumerator SpawnEmmiter()
        {
            yield return new WaitForSeconds(CalculateSpawnDelay());
            SpawnEnemie();
            currentTickNumber++;
            StartCoroutine("SpawnEmmiter");
        } 

        private float CalculateSpawnDelay()
        {
            if (currentTickNumber >= spawnerConfig.TotalTickNumber) return spawnerConfig.MinSpawnDelay;
            var currentSpawnDelay = spawnerConfig.BaseSpawnDelay - currentTickNumber * spawnDelayStep;
            return currentSpawnDelay;
        }

        private void SpawnEnemie()
        {
            for (int i = 0; i < spawnerConfig.Units.Count; i++)
            {
                float chance = Random.Range(0f, 100f);
                var currentChance = 0f;

                if (currentTickNumber == spawnerConfig.TotalTickNumber) currentChance = spawnerConfig.EndSpawnChance[i];
                else
                {
                    spawnChanceIncreaseStep = (spawnerConfig.EndSpawnChance[i] - spawnerConfig.BaseSpawnChance[i]) / spawnerConfig.TotalTickNumber;
                    currentChance = spawnerConfig.BaseSpawnChance[i] + 
                     currentTickNumber * spawnChanceIncreaseStep;
                }

                if (chance <= currentChance)
                {
                    var spawnPosition = ChangeSpawnPosition();
                    if (spawnPosition == Vector3.zero) return;

                    var enemie = enemiePool.GetEnemie(spawnerConfig.Units[i]);
                    enemie.SetActive(true);
                    enemie.transform.position = spawnPosition;
                }
            }
        }

        private Vector3 ChangeSpawnPosition()
        {
            var targetPositions = tileFinder.FindValidPositions(spawnerConfig.BoundSize, player.transform.position);

            if (targetPositions.Count == 0) return Vector3.zero;

            Vector3 positionForSpawn = targetPositions[Random.Range(0, targetPositions.Count - 1)] + Vector3.up;
            Vector2 positionForSpawn2d = positionForSpawn;
            return positionForSpawn2d;
        }
    }
}
