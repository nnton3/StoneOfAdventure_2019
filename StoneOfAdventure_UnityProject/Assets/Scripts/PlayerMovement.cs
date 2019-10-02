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
        private Rigidbody2D rb;
        private float moveHorizontal;
        private float moveVertical;
        [SerializeField] private float playerMovespeed = 1f;
        private bool isGrounded;
        [SerializeField] private float jumpPower;
        [SerializeField] private bool onLadder;
        [SerializeField] private bool canClimbDown;
        private Animator anim;
        private float direction = 0f;
        public float Direction  => direction;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            Debug.Log(canClimbDown);
            MoveLogic();
            PlayMoveAnimation();
            ClimbLogic();
            JumpLogic();

            Vector2 currentMovespeed = new Vector2(moveHorizontal, moveVertical);
            rb.velocity = currentMovespeed;
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
            if (!onLadder)
            {
                float playerMoveInput = Input.GetAxis("Horizontal");
                if (playerMoveInput != 0f) direction = (playerMoveInput > 0) ? 1 : -1;
                else direction = 0f;
                moveHorizontal = direction * playerMovespeed;
            }
        }

        private void ClimbLogic()
        {
            if (canClimb)
            {
                float playerClimbInput = Input.GetAxis("Vertical");

                if (playerClimbInput != 0)
                {
                    onLadder = true;
                    rb.bodyType = RigidbodyType2D.Kinematic;
                }

                if (onLadder)
                {
                    if (!canClimbDown && playerClimbInput < 0f)
                    {
                        moveVertical = 0f;
                        onLadder = false;
                        rb.bodyType = RigidbodyType2D.Dynamic;
                    }
                    else moveVertical = playerClimbInput * playerMovespeed;
                }
                else moveVertical = rb.velocity.y;
            }
            else moveVertical = rb.velocity.y;
        }

        private void PlayMoveAnimation()
        {
            anim.SetBool("moveHorizontal", Math.Abs(moveHorizontal) == playerMovespeed);
            anim.SetFloat("moveVertical", moveVertical);
            anim.SetBool("climb", onLadder);
        }

        [SerializeField] private bool canClimb;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder")) canClimb = true;
            if (collision.CompareTag("LadderStopper")) canClimbDown = false;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder"))
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                canClimb = false;
                onLadder = false;
            }

            if (collision.CompareTag("LadderStopper")) canClimbDown = true;
        }
    }
}
