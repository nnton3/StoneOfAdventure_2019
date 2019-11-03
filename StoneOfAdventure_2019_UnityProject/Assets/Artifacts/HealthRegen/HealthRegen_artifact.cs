using StoneOfAdventure.Combat;
using UnityEngine;

public class HealthRegen_artifact : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float periodicity;
    [SerializeField] private float healValue;
    Health Health;

    private void Start()
    {
        player = FindObjectOfType<PlayerStateController>().gameObject;
    }

    public void AddRegen()
    {
        HealthRegen healthRegen = player.AddComponent<HealthRegen>();
        healthRegen.Periodicity = periodicity;
        healthRegen.HealValue = healValue;
        healthRegen.StartHeal();
        Destroy(gameObject);
    }
}
