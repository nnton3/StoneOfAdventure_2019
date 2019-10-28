using UnityEngine;
using System.Collections;
using System;
using StoneOfAdventure.Core;

namespace StoneOfAdventure.Combat
{
    public class Health : MonoBehaviour, IDamaged
    {
        private Unit unit;

        private void Start()
        {
            unit = GetComponent<Unit>();
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
            else healthPoints -= damage;
        }

        private bool IsDead(float damage)
        {
            return HealthPoints <= damage;
        }
    }
}
