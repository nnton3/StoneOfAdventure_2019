using UnityEngine;

namespace StoneOfAdventure.Combat
{
    public class Fighter : MonoBehaviour
    {
        protected Animator anim;

        protected virtual void Start()
        {
            anim = GetComponent<Animator>();
        }

        public virtual void StartAttack()
        {
            anim.SetTrigger("attack");
        }

        public void Cancel()
        {
            anim.SetTrigger("disable");
        }
    }
}
