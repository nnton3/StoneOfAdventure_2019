using StoneOfAdventure.Core;
using UnityEngine;

namespace StoneOfAdventure.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Flip))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    public class Mover : MonoBehaviour, IAction
    {
        protected Rigidbody2D rb;
        protected Animator anim;
        protected Flip flip;
        private ActionScheduler scheduler;
        [SerializeField] protected float movespeed;

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            flip = GetComponent<Flip>();
            scheduler = GetComponent<ActionScheduler>();
        }

        internal void MoveTo(float direction)
        {
            Debug.Log("moving");
            scheduler.StartAction(this);
            flip.CheckDirection(direction);
            Vector2 horizontalMove = Vector2.right * direction * movespeed;
            rb.AddForce(horizontalMove, ForceMode2D.Force);
            anim.SetBool("moveHorizontal", true);
        }

        public void Cancel()
        {
            rb.AddForce(Vector2.zero);
            anim.SetBool("moveHorizontal", false);
        }
    }
}
