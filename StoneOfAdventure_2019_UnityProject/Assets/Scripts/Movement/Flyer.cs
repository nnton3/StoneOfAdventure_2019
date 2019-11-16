using UnityEngine;

namespace StoneOfAdventure.Movement
{
    public class Flyer : MonoBehaviour
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

        public void FlyTo(Vector2 direction, float movespeed)
        {
            flip.CheckDirection(direction.x);
            Vector2 moveVector = direction * movespeed;
            rb.velocity = moveVector;
            if(anim != null) anim.SetBool("moveHorizontal", true);
        }

        public void CancelFly()
        {
            rb.velocity = Vector2.zero;
            if (anim != null) anim.SetBool("moveHorizontal", false);
        }
    }
}
