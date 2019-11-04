using UnityEngine;
using System.Collections;

namespace StoneOfAdventure.Combat
{
    public class HealthRegen : MonoBehaviour
    {
        private Health health;

        public float Periodicity = 1f;  // TODO guard this variables
        public float HealValue = 1f;

        private void Start()
        {
            health = GetComponent<Health>();

            InvokeRepeating("Heal", 0f, Periodicity);
        }

        private void Heal()
        {
            health.Heal(HealValue);
        }
    }
}
