using StoneOfAdventure.Combat;
using UnityEngine.UI;
using UnityEngine;

namespace StoneOfAdventure.UI
{
    public class HPBar_manualSetUnit : HPBar
    {
        [SerializeField] private GameObject unit;

        protected override void Start()
        {
            if (unit == null) return;
            hpBar = GetComponent<Slider>();
            health = unit.GetComponent<Health>();

            health.MaxHealthUpdated.AddListener(UpdateHPBar);
            health.HPUpdated.AddListener(UpdateHPBar);
        }
    }
}
