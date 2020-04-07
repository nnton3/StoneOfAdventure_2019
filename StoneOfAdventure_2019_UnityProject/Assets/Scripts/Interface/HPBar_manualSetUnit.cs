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

            health.MaxHealthUpdated.AddListener(UpdateHPBar);
            health.HPDecreased.AddListener(UpdateHPBar);
            health.HPIncreased.AddListener(UpdateHPBar);
            UpdateHPBar();
        }

        protected override void UpdateHPBar(int value)
        {
            base.UpdateHPBar();
            HPValueText.text = $"{value}/{health.MaxHealthPoints}";
        }
    }
}
