using UnityEngine;

namespace StoneOfAdventure.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Flip))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    public class Mover : MonoBehaviour
    {
        protected Rigidbody2D rb;
        protected Animator anim;
        protected Flip flip;

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            flip = GetComponent<Flip>();
        }

        internal virtual void MoveTo(float direction, float movespeed)
        {
            flip.CheckDirection(direction);
            Vector2 horizontalMove = Vector2.right * direction * movespeed;
            rb.velocity = horizontalMove;
            anim.SetBool("moveHorizontal", true);
        }

        internal void MoveInAirTo(float direction, float movespeed)
        {
            flip.CheckDirection(direction);
            Vector2 horizontalMove = new Vector2(direction * movespeed, 0f);
            rb.AddForce(horizontalMove, ForceMode2D.Force);
        }

        public void Cancel()
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("moveHorizontal", false);
        }
    }
}
