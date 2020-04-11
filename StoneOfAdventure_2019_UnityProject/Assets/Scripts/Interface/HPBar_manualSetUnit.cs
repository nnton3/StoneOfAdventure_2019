using StoneOfAdventure.Combat;
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
            Initialize(unit);
        }

        public void Initialize(GameObject target)
        {
            hpBar = GetComponent<Slider>();
            health = target.GetComponent<Health>();
            HPValueText = GetComponentInChildren<Text>();

            health.HealthUpdated.AddListener(UpdateHPBar);
            UpdateHPBar();
        }

        protected override void UpdateHPBar()
        {
            base.UpdateHPBar();
            HPValueText.text = $"{health.HealthPoints}/{health.MaxHealthPoints}";
        }
    }
}
