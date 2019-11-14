using UnityEngine;
using UnityEngine.Events;

namespace StoneOfAdventure.Combat
{
    public class Fighter : MonoBehaviour
    {
        protected Animator anim;
        [HideInInspector] public UnityEvent Attack;

        protected virtual void Start()
        {
            anim = GetComponent<Animator>();
        }

        public virtual void StartAttack()
        {
            Attack.Invoke();
            anim.SetTrigger("attack");
        }

        public virtual void Cancel()
        {
            anim.SetTrigger("disable");
        }

        private void OnDisable()
        {
            Attack.RemoveAllListeners();
        }
    }
}
