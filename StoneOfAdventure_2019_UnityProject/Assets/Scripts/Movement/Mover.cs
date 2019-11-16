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
        private float currentMovespeedScale = 1f;
        public float CurrentMovespeedScale => currentMovespeedScale;

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            flip = GetComponent<Flip>();
        }

        internal virtual void MoveTo(float direction, float movespeed)
        {
            flip.CheckDirection(direction);
            Vector2 horizontalMove = Vector2.right * direction * movespeed * currentMovespeedScale;
            rb.velocity = horizontalMove;
            anim.SetBool("moveHorizontal", true);
        }

        internal void MoveInAirTo(float direction, float movespeed)
        {
            flip.CheckDirection(direction);
            Vector2 horizontalMove = new Vector2(direction * movespeed * currentMovespeedScale, 0f);
            rb.AddForce(horizontalMove, ForceMode2D.Force);
        }

        public void ModifyMovespeed(float addedMovespeedInPercent)
        {
            Debug.Log("work");
            currentMovespeedScale += addedMovespeedInPercent;
            anim.SetFloat("currentMovespeed", currentMovespeedScale);
        }

        public void CancelMove()
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("moveHorizontal", false);
        }
    }
}
