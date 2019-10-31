using StoneOfAdventure.Combat;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace StoneOfAdventure.UI
{
    public class HPBar : MonoBehaviour
    {
        private Health health;
        private Slider hpBar;

        private void Start()
        {
            health = GetComponentInParent<Health>();
            hpBar = GetComponent<Slider>();

            health.MaxHealthUpdated.AddListener(UpdateHPBar);
            health.HPUpdated.AddListener(UpdateHPBar);
        }

        private void UpdateHPBar()
        {
            hpBar.maxValue = health.MaxHealthPoints;
            hpBar.value = health.HealthPoints;
        }
    }
}
