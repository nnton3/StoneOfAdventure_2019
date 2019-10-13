using UnityEngine;
using StoneOfAdventure.Movement;

namespace StoneOfAdventure.Combat
{
    public class Fighter : MonoBehaviour
    {
        private Animator anim;

        protected virtual void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void StartAttack()
        {
            anim.SetTrigger("attack");
        }

        public void Cancel()
        {
            anim.ResetTrigger("attack");
        }
    }
}
