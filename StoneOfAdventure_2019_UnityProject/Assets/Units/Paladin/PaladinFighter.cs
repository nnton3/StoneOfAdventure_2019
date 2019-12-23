using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;
using System;

namespace StoneOfAdventure.Combat
{
    public class PaladinFighter : Fighter
    {
        private Transform player;

        [SerializeField] private float meleeRange;

        protected override void Start()
        {
            base.Start();
            player = FindObjectOfType<PaladinStateController>().transform;
        }

        public override void StartAttack()
        {
            if (TargetInMelee()) StartMeleeAttack();

            StartRangeAttack();
        }

        private void StartRangeAttack()
        {
            Attack?.Invoke();
            anim.SetTrigger("rangeAttack");
        }

        private void StartMeleeAttack()
        {
            Attack?.Invoke();
            anim.SetTrigger("meleeAttack");
        }

        private bool TargetInMelee()
        {
            return (player.position.x - transform.position.x) <= meleeRange;
        }
    }
}
