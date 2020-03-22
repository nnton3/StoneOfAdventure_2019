using UnityEngine;
using Zenject;

namespace StoneOfAdventure.Combat
{
    public class CriticalDamage_damageModifier : MonoBehaviour
    {
        [Inject(Id = "Player")] private Fighter fighter;
        [Inject] private DiContainer Container;
        private float addedDamageInPercent = 0.5f;
        private float criticalChance = 50f;

        public void Initialize(float _damageScale, float _criticalChance)
        {
            addedDamageInPercent = _damageScale;
            criticalChance = _criticalChance;

            Container.Inject(this);
        }

        private void Start()
        {
            fighter.AddModifierOfDamage(CalculateDamageScale);
        }

        private void CalculateDamageScale(ref int damage)
        {
            float chance = Random.Range(0f, 100f);
            if (chance <= criticalChance)
            {
                damage += (int)(fighter.BaseDamage * addedDamageInPercent);
            }
        }
    }
}
