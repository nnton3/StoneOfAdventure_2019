using UnityEngine;
using UnityEngine.Events;
using System;
using StoneOfAdventure.Core;

namespace StoneOfAdventure.Combat
{
    public class Health : MonoBehaviour, IDamaged
    {
        private Unit unit;

        [HideInInspector] public UnityEvent HPDecreased;
        [HideInInspector] public UnityEvent HPIncreased;
        [HideInInspector] public UnityEvent MaxHealthUpdated;

        [SerializeField] private float healthPoints = 100f;
        public float HealthPoints => healthPoints;
        private float maxHealthPoints;
        public float MaxHealthPoints => maxHealthPoints;
        [SerializeField] private bool untouchable = false;

        private void Start()
        {
            unit = GetComponent<Unit>();

            UpdateMaxHealthPoints(healthPoints);
        }

        public void ApplyDamage(float damage)
        {
            if (HealthPoints == 0) return;
            if (untouchable) return;
            if (IsDead(damage))
            {
                unit.Dead();
                healthPoints = 0f;
            }
            else
            {
                healthPoints -= damage;
            }
            HPDecreased.Invoke();
        }

        private bool IsDead(float damage)
        {
            return HealthPoints <= damage;
        }

        public void Heal(float healValue)
        {
            if (healthPoints < maxHealthPoints && healthPoints > 0f) healthPoints += healValue;
            HPIncreased.Invoke();
        }

        public void UpdateMaxHealthPoints(float value)
        {
            maxHealthPoints = value;
            MaxHealthUpdated.Invoke();
        }

        public void SwapUntouchable()
        {
            untouchable = !untouchable;
        }

        private void OnDisable()
        {
            HPDecreased.RemoveAllListeners();
            HPIncreased.RemoveAllListeners();
            MaxHealthUpdated.RemoveAllListeners();
        }
    }
}
