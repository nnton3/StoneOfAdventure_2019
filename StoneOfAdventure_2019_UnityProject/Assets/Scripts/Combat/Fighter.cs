using UnityEngine;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Core;

namespace StoneOfAdventure.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        private Animator anim;
        private Mover mover;
        private Flip flip;
        private ActionScheduler scheduler;

        private void Start()
        {
            anim = GetComponent<Animator>();
            mover = GetComponent<Mover>();
            flip = GetComponent<Flip>();
            scheduler = GetComponent<ActionScheduler>();
        }

        public void StartAttack()
        {
            scheduler.StartAction(this);
            anim.SetTrigger("attack");
        }

        public void Cancel() { }
    }
}
