using UnityEngine;
using StoneOfAdventure.Core;

namespace StoneOfAdventure.Movement
{
    public class Jump : MonoBehaviour
    {
        private bool isGrounded;
        private Rigidbody2D rb;
        private bool inTheAir = false;
        private Unit unit;
        private Animator anim;
        private JumpState jumpState;
        private MoveVerticalState moveVertical;

        private void Start()
        {
            unit = GetComponent<Unit>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            jumpState = GetComponent<JumpState>();
            moveVertical = GetComponent<MoveVerticalState>();
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

        void IsGroundedUpdate(Collision2D collision, bool value)
        {
            if (collision.gameObject.tag == ("Ground")) isGrounded = value;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            IsGroundedUpdate(collision, true);
            anim.SetBool("jump", false);
            if (inTheAir)
            {
                unit.DisableState();
                inTheAir = false;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            IsGroundedUpdate(collision, false);
            if (unit.State != moveVertical)
            {
                anim.SetBool("jump", true);
                if (unit.State != jumpState)
                {
                    unit.PlayerFell();
                    inTheAir = true;
                }
            }
        }
    }
}
