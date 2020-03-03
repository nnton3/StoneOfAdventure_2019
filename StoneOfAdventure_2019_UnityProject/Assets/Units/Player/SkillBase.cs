using UnityEngine;
using UnityEngine.Events;

namespace StoneOfAdventure.Combat
{
    public class SkillBase : MonoBehaviour
    {
        [SerializeField] private float coolDown = 2f;
        public float CoolDown => coolDown;
        [SerializeField] protected int baseDamage = 10;
        private float lastTimeUse;

        private bool canUseSkill = true;
        public bool CanUseSkill => canUseSkill;

        [HideInInspector] public UnityEvent SkillUsed;

        private void Update()
        {
            if (!canUseSkill) UpdateCoolDown();
        }

        public virtual void StartUse()
        {
            lastTimeUse = 0f;
            canUseSkill = false;
            SkillUsed.Invoke();
        }

        public void IncreaseBaseDamage(float increaseDamage)
        {
            baseDamage += (int)(baseDamage * increaseDamage);
        }
    
        private void UpdateCoolDown()
        {
            lastTimeUse += Time.deltaTime;
            if (lastTimeUse >= coolDown) canUseSkill = true;
        }

        public void ReduceCoolDown(float value)
        {
            lastTimeUse += value;
        }
    }
}
