using UnityEngine;

namespace StoneOfAdventure.Combat
{
    public class BatFang_attackEffect : MonoBehaviour
    {
        #region Variables
        private float lifestealInPersent = 0f;
        private Fighter fighter;
        private Health health;
        #endregion

        public void Initialize(float lifestealInPersent)
        {
            this.lifestealInPersent = lifestealInPersent;
        }

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();

            fighter.AddEffectOfAttack(ApplyLifesteal);
        }

        private void ApplyLifesteal(GameObject target)
        {
            Debug.Log("Lifestealed");
            var targetHealth = target.GetComponent<Health>();
            var lifestealedHP = (int)(targetHealth.HealthPoints * lifestealInPersent);
            targetHealth.ApplyDamage(lifestealedHP);
            health.Heal(lifestealedHP);
        }
    }
}
