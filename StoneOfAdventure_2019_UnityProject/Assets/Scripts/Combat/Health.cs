using UnityEngine;
using System.Collections;
using System;
using StoneOfAdventure.Core;

namespace StoneOfAdventure.Combat
{
    public class Health : MonoBehaviour, IDamaged
    {
        private Unit unit;
        private float maxHealthPosints;

        [SerializeField] private float healthPoints = 100f;
        public float HealthPoints => healthPoints;

        private void Start()
        {
            unit = GetComponent<Unit>();

            UpdateMaxHealthPoints(healthPoints);
        }

        public void ApplyDamage(float damage)
        {
            if (IsDead(damage))
            {
                unit.Dead();
                healthPoints = 0f;
            }
            else healthPoints -= damage;
        }

        private bool IsDead(float damage)
        {
            return HealthPoints <= damage;
        }

        public void Heal(float healValue)
        {
            if (healthPoints < maxHealthPosints) healthPoints += healValue;
        }

        public void UpdateMaxHealthPoints(float value)
        {
            maxHealthPosints = value;
        }
    }
}
