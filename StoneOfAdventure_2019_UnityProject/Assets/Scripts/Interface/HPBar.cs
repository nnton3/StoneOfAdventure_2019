using StoneOfAdventure.Combat;
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
            
            health.HealthUpdated.AddListener(UpdateHPBar);
            UpdateHPBar();
        }

        protected virtual void UpdateHPBar()
        {
            hpBar.maxValue = health.MaxHealthPoints;
            hpBar.value = health.HealthPoints;
        }
    }
}
