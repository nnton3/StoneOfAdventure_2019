using StoneOfAdventure.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace StoneOfAdventure.Core
{
    public class PlayerLevelObserver : MonoBehaviour
    {
        #region Variables
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
        #endregion

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
            playerHealth.Heal(playerHealth.MaxHealthPoints.Value - playerHealth.HealthPoints.Value);
            player.GetComponent<Fighter>().IncreaseBaseDamage(damageGetForLevel);
            var skills = player.GetComponents<SkillBase>();
            foreach (var skill in skills)
            {
                skill.IncreaseBaseDamage(damageGetForLevel);
            }
            baseExpirienceNeedToLevelUp += (int)(increaseLevelUpExperience * baseExpirienceNeedToLevelUp);
            UpdateMaxExperience?.Invoke();
            Instantiate(Resources.Load("LevelUp"), player.transform.position, Quaternion.identity);
        }
    }
}
