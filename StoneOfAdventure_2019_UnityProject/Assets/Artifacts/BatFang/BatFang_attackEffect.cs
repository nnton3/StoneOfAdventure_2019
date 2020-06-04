using StoneOfAdventure.Core;
using UnityEngine;
using Zenject;

namespace StoneOfAdventure.Combat
{
    public class BatFang_attackEffect : MonoBehaviour
    {
        #region Variables
        private float lifestealInPersent = 0f;
        [Inject(Id = "Player")] private Fighter fighter;
        [Inject(Id = "BloodShard")] private ObjectPool bloodShardsPool;
        [Inject] private DiContainer container;
        #endregion

        public void Initialize(float lifestealInPersent)
        {
            container.Inject(this);
            this.lifestealInPersent = lifestealInPersent;
        }

        private void Start()
        {
            fighter.AddEffectOfAttack(ApplyLifesteal);
        }

        private void ApplyLifesteal(GameObject target)
        {
            var targetHealth = target.GetComponent<Health>();
            var lifestealedHP = (int)(targetHealth.HealthPoints.Value * lifestealInPersent);
            targetHealth.ApplyDamage(lifestealedHP);
            Debug.Log(lifestealedHP);
            for (int i = 0; i < lifestealedHP; i++)
            {
                var bloodShard = bloodShardsPool.GetObject();
                bloodShard.transform.position = target.transform.position;
                bloodShard.SetActive(true);
                Debug.Log("work");
            }
        }
    }
}
