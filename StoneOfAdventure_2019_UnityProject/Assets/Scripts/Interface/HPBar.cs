using StoneOfAdventure.Combat;
using System.Collections;
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
            
            health.MaxHealthUpdated.AddListener(UpdateHPBar);
            health.HPDecreased.AddListener(UpdateHPBar);
            health.HPIncreased.AddListener(UpdateHPBar);
            UpdateHPBar();
        }

        protected virtual void UpdateHPBar()
        {
            hpBar.maxValue = health.MaxHealthPoints;
            hpBar.value = health.HealthPoints;
        }
    }
}
