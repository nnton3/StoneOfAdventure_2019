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
            health.HPUpdated.AddListener(UpdateHPBar);
            UpdateHPBar();
        }

        protected void UpdateHPBar()
        {
            hpBar.maxValue = health.MaxHealthPoints;
            hpBar.value = health.HealthPoints;
        }
    }
}
