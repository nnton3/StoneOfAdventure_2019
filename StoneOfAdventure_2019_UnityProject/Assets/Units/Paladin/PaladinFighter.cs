using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;
using System;

namespace StoneOfAdventure.Combat
{
    public class PaladinFighter : RaycastFighter
    {
        private Rigidbody2D rb;

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

        public void StartMelee2()
        {
            Attack.Invoke();
            anim.SetTrigger("melee2");
        }

        public void StartMelee3()
        {
            Attack.Invoke();
            anim.SetTrigger("melee3");
        }
    }
}
