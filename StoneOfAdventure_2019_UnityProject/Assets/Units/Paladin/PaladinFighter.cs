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

        [Header("MELEE ATTACK 2 SETTINGS")]
        [SerializeField] private int damageMelee2;
        [SerializeField] private Vector3 melee2AreaCenter;
        [SerializeField] private Vector3 melee2Area;
        [SerializeField] private bool melee2AreaVisible;

        [Header("MELEE ATTACK 3 SETTINGS")]
        [SerializeField] private int damageMelee3;
        [SerializeField] private Vector3 melee3AreaCenter;
        [SerializeField] private Vector3 melee3Area;
        [SerializeField] private bool melee3AreaVisible;

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            if (melee2AreaVisible)
                Gizmos.DrawWireCube(transform.position + melee2AreaCenter, melee2Area);
            if (melee3AreaVisible)
                Gizmos.DrawWireCube(transform.position + melee3AreaCenter, melee3Area);
        }

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
