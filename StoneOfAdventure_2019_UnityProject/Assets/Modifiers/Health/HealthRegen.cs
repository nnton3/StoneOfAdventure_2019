using UnityEngine;
using System.Collections;

namespace StoneOfAdventure.Combat
{
    public class HealthRegen : MonoBehaviour
    {
        private Health health;

        public int HealValue = 1;
        [SerializeField] private float temporaryRegen;
        private float temporaryRegenTime = 1f;

        private void Start()
        {
            health = GetComponent<Health>();

            UpdateRegen();
        }

        private void Heal()
        {
            health.Heal(1);
        }

        public void SetTemporaryRegen(float _temporaryRegen, float actionTime)
        {
            temporaryRegen = _temporaryRegen;
            temporaryRegenTime = actionTime;
            UpdateRegen();
            StopCoroutine("TemporaryRegenTimer");
            StartCoroutine("TemporaryRegenTimer");
        }

        IEnumerator TemporaryRegenTimer()
        {
            yield return new WaitForSeconds(temporaryRegenTime);
            temporaryRegen = 0f;
            UpdateRegen();
        }

        private void UpdateRegen()
        {
            CancelInvoke("Heal");
            InvokeRepeating("Heal", 0f, 1 / (HealValue + temporaryRegen));
        }
    }
}
