using UnityEngine;
using UnityEngine.Events;
using StoneOfAdventure.Core;
using StoneOfAdventure.UI;
using UniRx;

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
        public bool Untouchable => untouchable;

        public ReactiveProperty<int> HealthPoints { get; private set; }
        public ReactiveProperty<int> MaxHealthPoints { get; private set; }

        private ModifiersOfInputDamage applyModifiersOfInputDamage;
        private StoneOfAdventure.Core.Unit unit;
        private PointsUI pointsUI;

        [SerializeField] private bool untouchable = false;
        #endregion

        private void Awake()
        {
            unit = GetComponent<StoneOfAdventure.Core.Unit>();
            pointsUI = GetComponentInChildren<PointsUI>();

            HealthPoints = new ReactiveProperty<int>(100);
            MaxHealthPoints = new ReactiveProperty<int>(HealthPoints.Value);
        }

        public void ResetParams()
        {
            untouchable = true;
            HealthPoints.Value = MaxHealthPoints.Value;
            HealthUpdated.Invoke();
        }

        public void ApplyDamage(int damage)
        {
            if (HealthPoints.Value == 0) return;
            if (Untouchable) return;

            var currentDamage = damage;
            applyModifiersOfInputDamage?.Invoke(ref currentDamage);

            if (IsDead(currentDamage))
            {
                unit.Dead();
                HealthPoints.Value = 0;
            }
            else
            {
                HealthPoints.Value -= currentDamage;
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
            return HealthPoints.Value <= damage;
        }

        public void Heal(int healValue)
        {
            if (HealthPoints.Value != MaxHealthPoints.Value)
                pointsUI.CreatePointsUI(healValue.ToString(), Color.green);

            if (HealthPoints.Value < MaxHealthPoints.Value && HealthPoints.Value > 0f)
                HealthPoints.Value += healValue;

            HealthUpdated.Invoke();
            HPIncreased.Invoke(healValue);
        }

        /// <summary>
        /// Задать новое значение максимального количества очков здоровья
        /// </summary>
        /// <param name="newValue"></param>
        public void UpdateMaxHealthPoints(int newValue)
        {
            MaxHealthPoints.Value = newValue;
            HealthUpdated.Invoke();
            MaxHealthUpdated.Invoke();
        }

        /// <summary>
        /// Изменение значения максимального запаса здоровья в процентах
        /// </summary>
        /// <param name="increaseCurrentValue">На сколько процентов увеличить максимальное количество здоровья</param>
        public void UpdateMaxHealthPoints(float increaseCurrentValue)
        {
            MaxHealthPoints.Value += (int)(MaxHealthPoints.Value * increaseCurrentValue);
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
