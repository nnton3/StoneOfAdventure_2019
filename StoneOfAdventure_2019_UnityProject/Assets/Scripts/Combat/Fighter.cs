using UnityEngine;
using UnityEngine.Events;

namespace StoneOfAdventure.Combat
{
    public class Fighter : MonoBehaviour
    {
        protected Animator anim;
        protected Flip flip;
        [HideInInspector] public UnityEvent Attack;

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

        private void OnDisable()
        {
            Attack.RemoveAllListeners();
        }
    }
}
