using UnityEngine;
using UnityEngine.Events;
using System;
using StoneOfAdventure.Core;

namespace StoneOfAdventure.Combat
{
    public class Health : MonoBehaviour, IDamaged
    {
        private Unit unit;

        [HideInInspector] public UnityEvent HPUpdated;
        [HideInInspector] public UnityEvent MaxHealthUpdated;

        [SerializeField] private float healthPoints = 100f;
        public float HealthPoints => healthPoints;
        private float maxHealthPoints;
        public float MaxHealthPoints => maxHealthPoints;

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
                HPUpdated.Invoke();
            }
            else
            {
                healthPoints -= damage;
                HPUpdated.Invoke();
            }
        }

        private bool IsDead(float damage)
        {
            return HealthPoints <= damage;
        }

        public void Heal(float healValue)
        {
            if (healthPoints < maxHealthPoints && healthPoints > 0f) healthPoints += healValue;
            HPUpdated.Invoke();
        }

        public void UpdateMaxHealthPoints(float value)
        {
            maxHealthPoints = value;
            MaxHealthUpdated.Invoke();
        }

        private void OnDisable()
        {
            HPUpdated.RemoveAllListeners();
            MaxHealthUpdated.RemoveAllListeners();
        }
    }
}
