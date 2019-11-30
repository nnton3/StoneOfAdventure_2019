﻿using StoneOfAdventure.Combat;
using UnityEngine.UI;
using UnityEngine;

namespace StoneOfAdventure.UI
{
    public class HPBar_manualSetUnit : HPBar
    {
        [SerializeField] private GameObject unit;
        private Text HPValueText;

        protected override void Start()
        {
            if (unit == null) return;
            hpBar = GetComponent<Slider>();
            health = unit.GetComponent<Health>();
            HPValueText = GetComponentInChildren<Text>();

            health.MaxHealthUpdated.AddListener(UpdateHPBar);
            health.HPDecreased.AddListener(UpdateHPBar);
            health.HPIncreased.AddListener(UpdateHPBar);
            UpdateHPBar();
        }

        protected override void UpdateHPBar()
        {
            base.UpdateHPBar();
            HPValueText.text = $"{health.HealthPoints}/{health.MaxHealthPoints}";
        }
    }
}
