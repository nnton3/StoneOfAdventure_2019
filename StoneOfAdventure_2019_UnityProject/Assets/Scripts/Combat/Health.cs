using UnityEngine;
using UnityEngine.Events;
using StoneOfAdventure.Core;
using StoneOfAdventure.UI;

namespace StoneOfAdventure.Combat
{
    public class HPDecreasedEvent : UnityEvent<int> { }
    public class HPIncreasedEvent : UnityEvent<int> { }

    public class Health : MonoBehaviour, IDamaged
    {
        #region Variables
        [HideInInspector] public HPDecreasedEvent HPDecreased = new HPDecreasedEvent();
        [HideInInspector] public HPDecreasedEvent HPIncreased = new HPDecreasedEvent();
        [HideInInspector] public UnityEvent MaxHealthUpdated;
        [HideInInspector] public UnityEvent HealthUpdated;
        [HideInInspector] public UnityEvent Dead;

        public delegate void ModifiersOfInputDamage(ref int damage);
        public int HealthPoints => healthPoints;
        public int MaxHealthPoints => maxHealthPoints;
        public bool Untouchable => untouchable;

        private ModifiersOfInputDamage applyModifiersOfInputDamage;
        private Unit unit;
        private int maxHealthPoints;
        private PointsUI pointsUI;

        [SerializeField] private int healthPoints = 100;
        [SerializeField] private bool untouchable = false;
        #endregion

        private void Awake()
        {
            unit = GetComponent<Unit>();
            pointsUI = GetComponentInChildren<PointsUI>();
        }

        private void Start()
        {
            UpdateMaxHealthPoints(healthPoints);
        }

        public void ResetParams()
        {
            untouchable = true;
            healthPoints = MaxHealthPoints;
            HealthUpdated.Invoke();
        }

        public void ApplyDamage(int damage)
        {
            if (HealthPoints == 0) return;
            if (Untouchable) return;

            var currentDamage = damage;
            applyModifiersOfInputDamage?.Invoke(ref currentDamage);

            if (IsDead(currentDamage))
            {
                unit.Dead();
                healthPoints = 0;
            }
            else
            {
                healthPoints -= currentDamage;
            }
            HealthUpdated.Invoke();
            pointsUI.CreatePointsUI(currentDamage.ToString(), Color.yellow, 20);
            HPDecreased?.Invoke(currentDamage);
        }

        private bool CheckYourChance(float value)
        {
            var chance = Random.Range(0, 1);
            return chance < value;
        }
        
        private bool IsDead(float damage)
        {
            return HealthPoints <= damage;
        }

        public void Heal(int healValue)
        {
            if (healthPoints != maxHealthPoints)
                pointsUI.CreatePointsUI(healValue.ToString(), Color.green);

            if (healthPoints < maxHealthPoints && healthPoints > 0f)
                healthPoints += healValue;

            HealthUpdated.Invoke();
            HPIncreased.Invoke(healValue);
        }

        /// <summary>
        /// Задать новое значение максимального количества очков здоровья
        /// </summary>
        /// <param name="newValue"></param>
        public void UpdateMaxHealthPoints(int newValue)
        {
            maxHealthPoints = newValue;
            HealthUpdated.Invoke();
            MaxHealthUpdated.Invoke();
        }

        /// <summary>
        /// Изменение значения максимального запаса здоровья в процентах
        /// </summary>
        /// <param name="increaseCurrentValue">На сколько процентов увеличить максимальное количество здоровья</param>
        public void UpdateMaxHealthPoints(float increaseCurrentValue)
        {
            maxHealthPoints += (int)(maxHealthPoints * increaseCurrentValue);
            MaxHealthUpdated.Invoke();
        }

        public void SwapUntouchable()
        {
            untouchable = !Untouchable;
        }

        public void AddModifierOfInputDamage(ModifiersOfInputDamage modifier)
        {
            applyModifiersOfInputDamage += modifier;
        }

        private void OnDestroy()
        {
            HPDecreased.RemoveAllListeners();
            HPIncreased.RemoveAllListeners();
            MaxHealthUpdated.RemoveAllListeners();
        }
    }
}
