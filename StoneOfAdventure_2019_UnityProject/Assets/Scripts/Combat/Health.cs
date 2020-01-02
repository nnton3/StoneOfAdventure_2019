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

        internal void ApplyDamage(object damage)
        {
            throw new NotImplementedException();
        }

        [HideInInspector] public UnityEvent MaxHealthUpdated;

        [SerializeField] private int healthPoints = 100;
        public int HealthPoints => healthPoints;
        private int maxHealthPoints;
        public int MaxHealthPoints => maxHealthPoints;
        [SerializeField] private bool untouchable = false;

        private void Start()
        {
            unit = GetComponent<Unit>();

            UpdateMaxHealthPoints(healthPoints);
        }

        public void ApplyDamage(int damage)
        {
            if (HealthPoints == 0) return;
            if (untouchable) return;
            if (IsDead(damage))
            {
                unit.Dead();
                healthPoints = 0;
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

        public void Heal(int healValue)
        {
            if (healthPoints < maxHealthPoints && healthPoints > 0f) healthPoints += healValue;
            HPIncreased.Invoke();
        }

        public void UpdateMaxHealthPoints(int newValue)
        {
            maxHealthPoints = newValue;
            MaxHealthUpdated.Invoke();
        }

        public void UpdateMaxHealthPoints(float increaseCurrentValue)
        {
            maxHealthPoints += (int)(maxHealthPoints * increaseCurrentValue);
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
