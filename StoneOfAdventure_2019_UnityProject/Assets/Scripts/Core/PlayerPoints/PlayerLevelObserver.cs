using StoneOfAdventure.Combat;
using UnityEngine;
using UniRx;
using Zenject;

namespace StoneOfAdventure.Core
{
    public class PlayerLevelObserver : MonoBehaviour
    {
        #region Variables
        private GameObject player;

        [Inject] private SignalBus signalBus;
        [Inject] private MainLvlConfig config;
        [Inject] private LevelUpUI levelUpUI;

        public ReactiveProperty<int> CurrentExperience { get; private set; }
        public ReactiveProperty<int> ExpValueNeedToLevelUp { get; private set; } 
        #endregion

        private void Awake()
        {
            CurrentExperience = new ReactiveProperty<int>(0);
            ExpValueNeedToLevelUp = new ReactiveProperty<int>(config.ExpValueNeedToLevelUp);
        }

        private void Start()
        {
            player = FindObjectOfType<PlayerStateController>().gameObject;
        }

        public void UpdateExperienceValue(int experience)
        {
            CurrentExperience.Value += experience;
            if (CurrentExperience.Value >= ExpValueNeedToLevelUp.Value)
            {
                CurrentExperience.Value -= ExpValueNeedToLevelUp.Value;
                LevelUp();
            }
        }

        private void LevelUp()
        {
            signalBus.Fire<LevelUp>();
            ExpValueNeedToLevelUp.Value += (int)(config.IncreaseLevelUpExperience * ExpValueNeedToLevelUp.Value);
            levelUpUI.gameObject.SetActive(true);
        }
    }
}
