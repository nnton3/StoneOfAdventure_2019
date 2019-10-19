using System;
using UnityEngine;

namespace StoneOfAdventure.Movement
{
    public class Jump : MonoBehaviour
    {
        [SerializeField] private bool isGrounded;
        private Rigidbody2D rb;
        [SerializeField] private bool inTheAir = false;
        public bool InTheAir => inTheAir;
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
            anim.SetTrigger("jump");
        }

        internal void Cancel()
        {
            unit.DisableState();
            inTheAir = false;
            anim.SetTrigger("landed");
        }

        void IsGroundedUpdate(bool value)
        {
            isGrounded = value;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == ("Ground"))
            {
                IsGroundedUpdate(true);
                if (inTheAir) Cancel();
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == ("Ground"))
            {
                IsGroundedUpdate(false);
                if (unit.State != moveVertical)
                {
                    if (unit.State != jumpState)
                    {
                        unit.PlayerFell();
                        inTheAir = true;
                        anim.SetTrigger("jump");
                    }
                }
            }
        }
    }
}
