using UnityEngine;
using System.Collections.Generic;
using StoneOfAdventure.Combat;

public class ZombieBuffer : MonoBehaviour
{
    [SerializeField] private float healValue = 1f;
    [SerializeField] private float periodicity = 1f;

    private List<Health> healths = new List<Health>();

    private void Start()
    {
        InvokeRepeating("Heal", 0f, periodicity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemie"))
        {
            healths.Add(collision.GetComponent<Health>());
        }
    }

    private void Heal()
    {
        foreach (var unitHealth in healths)
        {
            unitHealth.Heal(healValue);
        }
    }
}
