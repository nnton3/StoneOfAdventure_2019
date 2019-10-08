using UnityEngine;
using System.Collections;
using StoneOfAdventure.Movement;
using System;

namespace StoneOfAdventure.Combat
{
    public class Fighter : MonoBehaviour
    {
        private EnemyDetector enemyDetector;
        private Animator anim;
        private Mover mover;
        private Flip flip;
        [SerializeField] private GameObject player;
        [SerializeField] private float attackRange;

        private void Start()
        {
            enemyDetector = GetComponentInChildren<EnemyDetector>();
            anim = GetComponent<Animator>();
            mover = GetComponent<Mover>();
            flip = GetComponent<Flip>();
        }

        private void Update()
        {
            if (player)
            {
                if (PlayerInAttackRange())
                {
                    if (PlayerInFront())
                    {
                        anim.SetTrigger("attack");
                        mover.MoveTo(0f);
                    }
                    else flip.FlipObject();
                }
                else
                {
                    mover.MoveTo(CalculateDirection());
                }
            }
        }

        private bool PlayerInFront()
        {
            if (flip.isFacingRight && CalculateDirection() == 1f ||
                !flip.isFacingRight && CalculateDirection() == -1f)
                return true;
            else
                return false;
        }

        private float CalculateDirection()
        {
            return Mathf.Sign(player.transform.position.x - transform.position.x);
        }

        private bool PlayerInAttackRange()
        {
            return Mathf.Abs(transform.position.x - player.transform.position.x) <= attackRange;
        }

        public void UpdateTarget()
        {
            player = enemyDetector.Player;
        }
    }
}
