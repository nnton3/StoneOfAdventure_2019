using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoneOfAdventure.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class PlayerMovement : MonoBehaviour
    {
        Rigidbody2D rb;
        float moveHorizontal;
        float moveVertical;
        [SerializeField] float playerMovespeed = 1f;
        private bool isGrounded;
        [SerializeField] private float jumpPower;
        private bool onLadder;
        private bool canClimbDown;
        Animator anim;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            MoveLogic();
            JumpLogic();
        }

        private void JumpLogic()
        {
            if (Input.GetAxis("Jump") > 0)
            {
                if (isGrounded) rb.AddForce(Vector2.up * jumpPower);
            }
        }

        void IsGroundedUpdate(Collision2D collision, bool value)
        {
            if (collision.gameObject.tag == ("Ground")) isGrounded = value;
        }

        private void OnCollisionEnter2D(Collision2D collision) { IsGroundedUpdate(collision, true); }

        private void OnCollisionExit2D(Collision2D collision) { IsGroundedUpdate(collision, false); }

        private void MoveLogic()
        {
            moveHorizontal = Input.GetAxis("Horizontal") * playerMovespeed;
            if (onLadder)
            {
                moveVertical = Input.GetAxis("Vertical") * playerMovespeed;
                if (moveVertical < 0)
                    if (!canClimbDown) moveVertical = 0f;
            }
            else moveVertical = rb.velocity.y;
            Vector2 currentMovespeed = new Vector2(moveHorizontal, moveVertical);
            rb.velocity = currentMovespeed;
            anim.SetFloat("moveHorizontal", moveHorizontal);
            anim.SetFloat("moveVertical", moveVertical);
            anim.SetBool("climb", onLadder);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder"))
            {
                onLadder = true;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }

            if (collision.CompareTag("LadderStopper")) canClimbDown = false;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder"))
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                onLadder = false;
            }

            if (collision.CompareTag("LadderStopper")) canClimbDown = true;
        }
    }
}
