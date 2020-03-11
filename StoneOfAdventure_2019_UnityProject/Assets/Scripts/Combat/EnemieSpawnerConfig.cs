using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName ="Combat")]
public class EnemieSpawnerConfig : ScriptableObject
{
    [SerializeField] private float baseSpawnDelay = 5f;
    [SerializeField] private float minSpawnDelay = 1f;
    [SerializeField] private List<string> units = new List<string>();
    [SerializeField] private List<float> baseSpawnChance = new List<float>();
    [SerializeField] private List<float> endSpawnChance = new List<float>();
    [SerializeField] private int totalTickNumber = 40;
}
