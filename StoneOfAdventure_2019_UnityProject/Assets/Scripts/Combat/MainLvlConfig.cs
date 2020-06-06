using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName ="NewSpawnConfig", menuName = "Create new spawn config")]
public class MainLvlConfig : ScriptableObject
{
    [SerializeField] private float baseSpawnDelay = 5f;
    [SerializeField] private float minSpawnDelay = 1f;
    [SerializeField] private List<string> units = new List<string>();
    [SerializeField] private List<float> baseSpawnChance = new List<float>();
    [SerializeField] private List<float> endSpawnChance = new List<float>();
    [SerializeField] private int totalTickNumber = 40;
    [SerializeField] private Vector3Int boundSize = new Vector3Int(10, 10, 1);

    public float BaseSpawnDelay => baseSpawnDelay;
    public float MinSpawnDelay => minSpawnDelay;
    public List<string> Units => units;
    public List<float> BaseSpawnChance => baseSpawnChance;
    public List<float> EndSpawnChance => endSpawnChance;
    public int TotalTickNumber => totalTickNumber;
    public Vector3Int BoundSize => boundSize;
    public int TargetLocationPointsValue = 100;
    public float HealthGetForLevel = 0.1f;
    public float IncreaseLevelUpExperience = 0.1f;
    public float DamageGetForLevel = 0.1f;
    public int ExpValueNeedToLevelUp = 100;
}
