using UnityEngine;
using StoneOfAdventure.Movement;
using Zenject;

namespace StoneOfAdventure.Combat
{
    public class PlayerSkill2 : SkillBase
    {
        #region Vatiables
        [SerializeField] private float movespeed = 10f;

        [Inject(Id = "Player")] private Animator anim;
        [Inject(Id = "Player")] private Rigidbody2D rb;
        [Inject(Id = "Player")] private Mover mover;
        [Inject(Id = "Player")] private Jump jump;
        [Inject(Id = "Player")] private Flip flip;
        [Inject(Id = "Player")] private PlayerStateController unit;
        private GameObject playerScill2Collider;
        #endregion

        private void Start()
        {
            playerScill2Collider = transform.Find("Skill2Collider").gameObject;

            playerScill2Collider.SetActive(false);
            playerScill2Collider.GetComponent<OneHitTrigger>().Initialize(baseDamage);
        }

        public override void StartUse()
        {
            base.StartUse();
            anim.SetTrigger("skill2");
        }

        // Animation event
        public void StartMove()
        {
            float direction = (flip.isFacingRight) ? 1f : -1f;
            mover.MoveTo(direction, movespeed);
            playerScill2Collider.SetActive(true);
        }

        // Animation event
        public void Skill2End()
        {
            playerScill2Collider.SetActive(false);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            mover.CancelMove();
            if (!jump.isGrounded)
            {
                unit.Fell();
                return;
            }
            unit.DisableState();
        }
    }
}
