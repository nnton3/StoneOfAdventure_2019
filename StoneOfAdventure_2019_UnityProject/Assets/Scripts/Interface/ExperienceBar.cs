using StoneOfAdventure.Core;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace StoneOfAdventure.UI
{ 
    public class ExperienceBar : MonoBehaviour
    {
        [Inject] protected PlayerLevelObserver levelObserver;
        protected Slider expBar;
        protected Text expValueText;

        protected virtual void Start()
        {
            expBar = GetComponent<Slider>();
            expValueText = GetComponentInChildren<Text>();

            levelObserver.CurrentExperience
                            .ObserveEveryValueChanged(x => x.Value)
                            .Subscribe(_ => UpdateEXBar())
                            .AddTo(this);
            levelObserver.ExpValueNeedToLevelUp
                            .ObserveEveryValueChanged(x => x.Value)
                            .Subscribe(_ => UpdateEXBar())
                            .AddTo(this);

            UpdateEXBar();
        }

        protected void UpdateEXBar()
        {
            expBar.maxValue = levelObserver.ExpValueNeedToLevelUp.Value;
            expBar.value = levelObserver.CurrentExperience.Value;
            expValueText.text = $"{levelObserver.CurrentExperience}/{levelObserver.ExpValueNeedToLevelUp.Value}";
        }
    }
}
