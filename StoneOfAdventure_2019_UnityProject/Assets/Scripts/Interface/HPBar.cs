using StoneOfAdventure.Combat;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace StoneOfAdventure.UI
{
    public class HPBar : MonoBehaviour
    {
        protected Health health;
        protected Slider hpBar;

        protected virtual void Start()
        {
            health = GetComponentInParent<Health>();
            hpBar = GetComponent<Slider>();

            health.HealthPoints
                .ObserveEveryValueChanged(x => x.Value)
                .Subscribe(_ => UpdateHPBar())
                .AddTo(this);

            UpdateHPBar();
        }

        protected virtual void UpdateHPBar()
        {
            hpBar.maxValue = health.MaxHealthPoints.Value;
            hpBar.value = health.HealthPoints.Value;
        }
    }
}
