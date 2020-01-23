using UnityEngine;
using UnityEngine.Events;
using System;
using StoneOfAdventure.Core;

namespace StoneOfAdventure.Combat
{
    public class Health : MonoBehaviour, IDamaged
    {
        #region Variables
        private Unit unit;

        [HideInInspector] public UnityEvent HPDecreased;
        [HideInInspector] public UnityEvent HPIncreased;
        [HideInInspector] public UnityEvent MaxHealthUpdated;

        [SerializeField] private int healthPoints = 100;
        public int HealthPoints => healthPoints;
        private int maxHealthPoints;
        public int MaxHealthPoints => maxHealthPoints;

        private float blockChance;
        public float BlockChance {
            get => blockChance;
            set
            {
                if (value >= 0 && value <= 1f)
                    blockChance = value;
            }
        }
        private float dodgeChance;
        public float DodgeChance { get => dodgeChance; set
            {
                if (value >= 0 && value <= 1f)
                    dodgeChance = value;
            }
        }
        private bool oneTimeBlock = false;

        [SerializeField] private bool untouchable = false;
        #endregion

        private void Start()
        {
            unit = GetComponent<Unit>();

            UpdateMaxHealthPoints(healthPoints);
        }

        public void ApplyDamage(int damage)
        {
            if (oneTimeBlock) { oneTimeBlock = false; return; }
            if (HealthPoints == 0) return;
            if (untouchable) return;
            if (dodgeChance != 0f)
            {
                if (CheckYourChance(dodgeChance)) return;
            }
            if (blockChance != 0f)
            {
                if (CheckYourChance(blockChance)) return;
            }
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

        private bool CheckYourChance(float value)
        {
            var chance = UnityEngine.Random.Range(0, 1);
            return chance < value;
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

        public void BlockNextDamage() { oneTimeBlock = true; }

        private void OnDisable()
        {
            HPDecreased.RemoveAllListeners();
            HPIncreased.RemoveAllListeners();
            MaxHealthUpdated.RemoveAllListeners();
        }
    }
}
