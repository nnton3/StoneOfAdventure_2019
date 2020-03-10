using StoneOfAdventure.Core;
using UnityEngine;
using UnityEngine.UI;

namespace StoneOfAdventure.UI
{ 
    public class ExperienceBar : MonoBehaviour
    {
        protected PlayerLevelObserver levelObserver;
        protected Slider expBar;
        protected Text expValueText;

        protected virtual void Start()
        {
            levelObserver = GetComponentInParent<PlayerLevelObserver>();
            expBar = GetComponent<Slider>();
            expValueText = GetComponentInChildren<Text>();

            levelObserver.UpdateCurrentExperience.AddListener(UpdateEXBar);
            levelObserver.UpdateMaxExperience.AddListener(UpdateEXBar);
            UpdateEXBar();
        }

        protected void UpdateEXBar()
        {
            expBar.maxValue = levelObserver.MaxExperience;
            expBar.value = levelObserver.CurrentExperience;
            expValueText.text = $"{levelObserver.CurrentExperience}/{levelObserver.MaxExperience}";
        }
    }
}
