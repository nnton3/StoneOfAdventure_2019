using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoneOfAdventure.Movement
{
    public class PlayerMovement : Mover
    {
        private float moveHorizontal;
        private float moveVertical;
        [SerializeField] private float playerMovespeedInAir = 1f;
        private bool isGrounded;
        [SerializeField] private float jumpPower;
       
        private float direction = 0f;
        public float Direction  => direction;

        private void FixedUpdate()
        {
            MoveLogic();
            PlayMoveAnimation();
            ClimbLogic();
            JumpLogic();

            Vector2 currentMovespeed = new Vector2(moveHorizontal, moveVertical);

            bool NotInTheAir = isGrounded || onLadder;
            if (NotInTheAir) rb.velocity = currentMovespeed;
            else rb.AddForce(currentMovespeed, ForceMode2D.Force);
        }

        private void JumpLogic()
        {
            if (Input.GetAxisRaw("Jump") > 0)
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
                float playerMoveInput = Input.GetAxisRaw("Horizontal");
                if (playerMoveInput != 0f)
                {
                    direction = (playerMoveInput > 0) ? 1 : -1;
                    flip.CheckDirection(direction);
                }
                else direction = 0f;
                moveHorizontal = direction * movespeed;
            }
            else moveHorizontal = 0f;
        }

        private void PlayMoveAnimation()
        {
            anim.SetBool("moveHorizontal", Math.Abs(moveHorizontal) == movespeed);
            anim.SetFloat("moveVertical", moveVertical);
            anim.SetBool("climb", onLadder);
        }

        [SerializeField] private bool onLadder;
        [SerializeField] private bool canClimbDown = true;
        [SerializeField] private bool canClimbUp = true;
        [SerializeField] private bool canClimb;

        private void ClimbLogic()
        {
            if (canClimb)
            {
                float playerClimbInput = Input.GetAxisRaw("Vertical");

                if (playerClimbInput != 0)
                {
                    onLadder = true;
                    rb.bodyType = RigidbodyType2D.Kinematic;
                }

                if (onLadder)
                {
                    if (LadderEnd(playerClimbInput))
                    {
                        StopVerticalMove();
                    }
                    else moveVertical = playerClimbInput * movespeed;
                }
                else moveVertical = rb.velocity.y;
            }
            else moveVertical = rb.velocity.y;
        }

        private void StopVerticalMove()
        {
            moveVertical = 0f;
            onLadder = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        private bool LadderEnd(float playerClimbInput)
        {
            return ((!canClimbDown && playerClimbInput < 0f) || (!canClimbUp && playerClimbInput > 0f));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder")) canClimb = true;
            if (collision.CompareTag("DownStopper")) canClimbDown = false;
            if (collision.CompareTag("UpperStopper")) canClimbUp = false;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder")) canClimb = false;
            if (collision.CompareTag("DownStopper")) canClimbDown = true;
            if (collision.CompareTag("UpperStopper")) canClimbUp = true;
        }
    }
}
