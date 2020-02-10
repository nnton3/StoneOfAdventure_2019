using UnityEngine;

namespace StoneOfAdventure.Combat
{
    public class SphereOfEnergy_damageModifier : MonoBehaviour
    {
        #region Variables
        private Fighter fighter;
        private PlayerStateController playerController;
        private float targetTimeOnFeet = 2f;
        private float bonusDamageInPercent = 1.5f;
        [SerializeField] private float currentTimeOnFeet = 0f;
        [SerializeField] private float minInterval = 0.2f;
        #endregion

        public void Initialize(float _targetTimeOnFeet, float _bonusDamageInPercent)
        {
            targetTimeOnFeet = _targetTimeOnFeet;
            bonusDamageInPercent = _bonusDamageInPercent;
        }

        private void Start()
        {
            fighter = GetComponent<AOE_Fighter>();
            playerController = GetComponent<PlayerStateController>();

            playerController.StartWalk.AddListener(StartOnFeetTimer);
            playerController.StopWalk.AddListener(StopOnFeetTimer);
            fighter.AddModifierOfDamage(CalculateAddedDamage);
        }

        private void StartOnFeetTimer()
        {
            InvokeRepeating("IncrementCurrentTimeInFeet", 0f, minInterval);
        }

        private void StopOnFeetTimer()
        {
            CancelInvoke("IncrementCurrentTimeInFeet");
        }

        private void IncrementCurrentTimeInFeet()
        {
            currentTimeOnFeet += minInterval;
        }

        private void CalculateAddedDamage(ref int damage)
        {
            if (currentTimeOnFeet >= targetTimeOnFeet)
            {
                currentTimeOnFeet = 0f;
                damage += (int)(fighter.BaseDamage * bonusDamageInPercent);
            }
        }
    }
}
