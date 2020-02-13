using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;
using System;

namespace StoneOfAdventure.Combat
{
    public class PaladinFighter : AOE_Fighter
    {
        private Rigidbody2D rb;
        [SerializeField] private int damageMelee1;
        [SerializeField] private int damageMelee2;
        [SerializeField] private int damageMelee3;

        protected override void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody2D>();
        }

        // Animation event
        public void AddImpulse(int power)
        {
            var dir = (flip.isFacingRight) ? 1 : -1;
            rb.AddForce(Vector2.right * dir * power, ForceMode2D.Impulse);
        }

        public override void StartAttack()
        {
            base.StartAttack();
            baseDamage = damageMelee1;
        }

        public void StartMelee2()
        {
            UseAttack.Invoke();
            baseDamage = damageMelee2;
            anim.SetTrigger("melee2");
        }

        public void StartMelee3()
        {
            UseAttack.Invoke();
            baseDamage = damageMelee3;
            anim.SetTrigger("melee3");
        }
    }
}
