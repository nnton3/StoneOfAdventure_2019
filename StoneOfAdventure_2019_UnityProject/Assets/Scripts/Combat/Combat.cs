using UnityEngine;
using System.Collections;
using StoneOfAdventure.Movement;
using System;

namespace StoneOfAdventure.Combat
{
    public class Combat : MonoBehaviour
    {
        private EnemyDetector enemyDetector;
        private Animator anim;
        private Mover mover;
        private GameObject player;
        [SerializeField] private float attackRange;

        private void Start()
        {
            enemyDetector = GetComponent<EnemyDetector>();
            anim = GetComponent<Animator>();
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (player)
            {
                if (PlayerInAttackRange()) anim.SetTrigger("attack");
                else mover.MoveTo(CalculateDirection());
            }
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
