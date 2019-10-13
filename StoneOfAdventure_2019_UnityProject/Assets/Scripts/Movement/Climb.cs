using UnityEngine;

namespace StoneOfAdventure.Movement
{
    public class Climb : MonoBehaviour
    {
        Rigidbody2D rb;
        private PlayerStateController unit;
        Vector2 moveVertical;

        [SerializeField] private bool onLadder;
        [SerializeField] private bool canClimbDown = true;
        [SerializeField] private bool canClimbUp = true;
        [SerializeField] public bool CanClimb { get; private set; }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            unit = GetComponent<PlayerStateController>();
        }

        public void TryToClimb(float direction, float verticalMovespeed)
        {
            onLadder = true;
            rb.bodyType = RigidbodyType2D.Kinematic;

            if (LadderEnd(direction)) StopVerticalMove();

            if (onLadder)
            {
                moveVertical = Vector2.up * direction * verticalMovespeed;
            }
            rb.velocity = moveVertical;
        }

        public void StopVerticalMove()
        {
            moveVertical = Vector2.zero;
            onLadder = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
            unit.DisableState();
        }

        public bool LadderEnd(float playerClimbInput)
        {
            return ((!canClimbDown && playerClimbInput < 0f) || (!canClimbUp && playerClimbInput > 0f));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder")) CanClimb = true;
            if (collision.CompareTag("DownStopper")) canClimbDown = false;
            if (collision.CompareTag("UpperStopper")) canClimbUp = false;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Ladder")) CanClimb = false;
            if (collision.CompareTag("DownStopper")) canClimbDown = true;
            if (collision.CompareTag("UpperStopper")) canClimbUp = true;
        }
    }
}
