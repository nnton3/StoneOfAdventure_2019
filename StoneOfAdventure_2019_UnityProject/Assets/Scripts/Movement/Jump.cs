using System;
using UnityEngine;

namespace StoneOfAdventure.Movement
{
    public class Jump : MonoBehaviour
    {
        private bool isGrounded;
        private Rigidbody2D rb;
        [SerializeField] private bool inTheAir = false;
        private PlayerStateController unit;
        private Animator anim;
        private PlayerJumpState jumpState;
        private PlayerMoveVerticalState moveVertical;

        private void Start()
        {
            unit = GetComponent<PlayerStateController>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            jumpState = GetComponent<PlayerJumpState>();
            moveVertical = GetComponent<PlayerMoveVerticalState>();
        }

        public void ToJump(Vector2 direction, float jumpPower)
        {
            if (isGrounded)
            {
                JumpLogic(direction, jumpPower);
            }
        }

        public void ToJumpOnLadder(Vector2 direction, float jumpPower)
        {
            JumpLogic(direction, jumpPower);
        }

        private void JumpLogic(Vector2 direction, float jumpPower)
        {
            rb.AddForce(direction * jumpPower);
            inTheAir = true;
        }

        internal void Cancel()
        {
            unit.DisableState();
            inTheAir = false;
            anim.SetBool("jump", inTheAir);
        }

        void IsGroundedUpdate(Collision2D collision, bool value)
        {
            if (collision.gameObject.tag == ("Ground")) isGrounded = value;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            IsGroundedUpdate(collision, true);
            if (inTheAir)
            {
                inTheAir = false;
                Cancel();
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            IsGroundedUpdate(collision, false);
            if (unit.State != moveVertical)
            {
                if (unit.State != jumpState)
                {
                    unit.PlayerFell();
                    inTheAir = true;
                    anim.SetBool("jump", inTheAir);
                }
            }
        }
    }
}
