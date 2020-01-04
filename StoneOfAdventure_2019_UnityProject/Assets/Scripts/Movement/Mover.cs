using UnityEngine;

namespace StoneOfAdventure.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Flip))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    public class Mover : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float baseMovespeed = 5f;
        public float BaseMovespeed => baseMovespeed;
        [SerializeField] private float movespeedInTheAir = 5f;

        protected Rigidbody2D rb;
        protected Animator anim;
        protected Flip flip;
        private float currentMovespeedScale = 1f;
        public float CurrentMovespeedScale => currentMovespeedScale;
        #endregion

        protected virtual void Awake()
        {
            flip = GetComponent<Flip>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        protected virtual void Start() { }

        internal virtual void MoveTo(float direction)
        {
            flip.CheckDirection(direction);
            Vector2 horizontalMove = Vector2.right * direction * baseMovespeed * currentMovespeedScale;
            rb.velocity = horizontalMove;
            anim.SetBool("moveHorizontal", true);
        }

        public void MoveTo(float direction, float _movespeed)
        {
            flip.CheckDirection(direction);
            Vector2 horizontalMove = Vector2.right * direction * _movespeed * currentMovespeedScale;
            rb.velocity = horizontalMove;
            anim.SetBool("moveHorizontal", true);
        }

        internal void MoveInAirTo(float direction)
        {
            flip.CheckDirection(direction);
            Vector2 horizontalMove = new Vector2(direction * movespeedInTheAir * currentMovespeedScale, 0f);
            rb.AddForce(horizontalMove, ForceMode2D.Force);
        }

        public void ModifyMovespeedScale(float addedMovespeedInPercent)
        {
            currentMovespeedScale += addedMovespeedInPercent;
            anim.SetFloat("currentMovespeed", currentMovespeedScale);
        }

        public void ModifyBaseMovespeed(float movespeedPoints)
        {
            if (baseMovespeed + movespeedPoints >= 1f)
                baseMovespeed += movespeedPoints;
        }

        public void CancelMove()
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("moveHorizontal", false);
        }
    }
}
