using UnityEngine;
using UnityEngine.Events;

namespace StoneOfAdventure.Combat
{
    [RequireComponent(typeof(Flip))]
    [RequireComponent(typeof(Animator))]
    public class Fighter : MonoBehaviour
    {
        protected Animator anim;
        protected Flip flip;
        [HideInInspector] public UnityEvent Attack;
        private float currentAttackSpeed = 1f;
        public float CurrentAttackSpeed => currentAttackSpeed;

        protected virtual void Start()
        {
            anim = GetComponent<Animator>();
            flip = GetComponent<Flip>();
        }

        public virtual void StartAttack()
        {
            Attack.Invoke();
            anim.SetTrigger("attack");
        }

        public virtual void CancelAttack()
        {
            anim.SetTrigger("disable");
        }

        public void ModifyAttackSpeed(float addedAttackspeedInPercent)
        {
            currentAttackSpeed += addedAttackspeedInPercent;
            anim.SetFloat("currentAttackspeed", currentAttackSpeed);
        }

        private void OnDisable()
        {
            Attack.RemoveAllListeners();
        }
    }
}
