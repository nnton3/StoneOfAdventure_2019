using UnityEngine;
using Zenject;

namespace StoneOfAdventure.Combat
{
    public class BatFang_attackEffect : MonoBehaviour
    {
        #region Variables
        private float lifestealInPersent = 0f;
        [Inject(Id = "Player")] private Fighter fighter;
        [Inject(Id = "Player")] private Health health;
        [Inject] DiContainer Container;
        #endregion

        public void Initialize(float lifestealInPersent)
        {
            this.lifestealInPersent = lifestealInPersent;

            Container.Inject(this);
        }

        private void Start()
        {
            fighter.AddEffectOfAttack(ApplyLifesteal);
        }

        private void ApplyLifesteal(GameObject target)
        {
            var targetHealth = target.GetComponent<Health>();
            var lifestealedHP = (int)(targetHealth.HealthPoints * lifestealInPersent);
            targetHealth.ApplyDamage(lifestealedHP);
            health.Heal(lifestealedHP);
        }
    }
}
