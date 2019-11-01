using StoneOfAdventure.Core;
using System;
using UnityEngine;

namespace StoneOfAdventure.Movement
{
    public class Jump : MonoBehaviour
    {
        public bool isGrounded { get; private set; }
        private Rigidbody2D rb;
        private Unit unit;
        private Animator anim;

        private void Start()
        {
            unit = GetComponent<Unit>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
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
            anim.SetTrigger("jump");
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
                unit.DisableState();
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == ("Ground"))
            {
                IsGroundedUpdate(false);
                unit.Fell();
            }
        }
    }
}
