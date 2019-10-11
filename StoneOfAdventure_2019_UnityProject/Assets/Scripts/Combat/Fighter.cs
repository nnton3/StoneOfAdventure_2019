using UnityEngine;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Core;

namespace StoneOfAdventure.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        private Animator anim;

        private void Start()
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
