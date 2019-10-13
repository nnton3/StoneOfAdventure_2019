using UnityEngine;
using System.Collections;
using System;

namespace StoneOfAdventure.Combat
{
    public class Health : MonoBehaviour, IDamaged
    {
        private ZombieStateController unit;
        private ZombieDeathState deathState;

        private void Start()
        {
            deathState = GetComponent<ZombieDeathState>();
            unit = GetComponent<ZombieStateController>();
        }

        [SerializeField] private float healthPoints = 100f;
        public float HealthPoints => healthPoints;

        public void ApplyDamage(float damage)
        {
            if (IsDead(damage))
            {
                unit.Dead();
                healthPoints = 0f;
            }
            healthPoints -= damage;
        }

        private bool IsDead(float damage)
        {
            return HealthPoints < damage;
        }
    }
}
