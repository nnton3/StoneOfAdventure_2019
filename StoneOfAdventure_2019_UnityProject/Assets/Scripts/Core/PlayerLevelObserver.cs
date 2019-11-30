using StoneOfAdventure.Combat;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Core
{
    public class PlayerLevelObserver : MonoBehaviour
    {
        [SerializeField] private int baseExpirienceNeedToLevelUp = 100;
        public int MaxExperience => baseExpirienceNeedToLevelUp;
        [SerializeField] private float damageGetForLevel = 0.1f;
        [SerializeField] private float healthGetForLevel = 0.1f;
        [SerializeField] private float increaseLevelUpExperience = 0.1f;
        [HideInInspector] public UnityEvent UpdateCurrentExperience;
        [HideInInspector] public UnityEvent UpdateMaxExperience;

        private int currentExperienceValue = 0;
        public int CurrentExperience => currentExperienceValue;
        private GameObject player;

        private void Start()
        {
            player = FindObjectOfType<PlayerStateController>().gameObject;
        }

        public void UpdateExperienceValue(int experience)
        {
            currentExperienceValue += experience;
            if (currentExperienceValue >= baseExpirienceNeedToLevelUp)
            {
                currentExperienceValue -= baseExpirienceNeedToLevelUp;
                LevelUp();
            }
            UpdateCurrentExperience?.Invoke();
        }

        private void LevelUp()
        {
            var playerHealth = player.GetComponent<Health>();
            playerHealth.UpdateMaxHealthPoints(healthGetForLevel);
            playerHealth.Heal(playerHealth.MaxHealthPoints - playerHealth.HealthPoints);
            player.GetComponent<Fighter>().IncreaseBaseDamage(damageGetForLevel);
            baseExpirienceNeedToLevelUp += (int)(increaseLevelUpExperience * baseExpirienceNeedToLevelUp);
            UpdateMaxExperience?.Invoke();
        }
    }
}
