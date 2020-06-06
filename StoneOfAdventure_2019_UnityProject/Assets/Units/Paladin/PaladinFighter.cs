using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;
using System;
using StoneOfAdventure.Core;

namespace StoneOfAdventure.Combat
{
    public class PaladinFighter : Fighter
    {
        #region Variables
        private Rigidbody2D rb;

        [Header("MELEE ATTACK 1 SETTINGS")]
        [SerializeField] private OneHitTrigger hitTrigger1;
        [SerializeField] private int damageMelee1;
        [SerializeField] private Vector3 melee1AreaCenter;
        [SerializeField] private Vector3 melee1Area;
        [SerializeField] private bool melee1AreaVisible;


        [Header("MELEE ATTACK 2 SETTINGS")]
        [SerializeField] private OneHitTrigger hitTrigger2;
        [SerializeField] private int damageMelee2;
        [SerializeField] private Vector3 melee2AreaCenter;
        [SerializeField] private Vector3 melee2Area;
        [SerializeField] private bool melee2AreaVisible;

        [Header("MELEE ATTACK 3 SETTINGS")]
        [SerializeField] private int damageMelee3;
        [SerializeField] private Vector3 melee3AreaCenter;
        [SerializeField] private Vector3 melee3Area;
        [SerializeField] private bool melee3AreaVisible;
        [SerializeField] private LayerMask layerMask;
        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (melee1AreaVisible)
                Gizmos.DrawWireCube(transform.position + melee1AreaCenter, melee1Area);
            if (melee2AreaVisible)
                Gizmos.DrawWireCube(transform.position + melee2AreaCenter, melee2Area);
            if (melee3AreaVisible)
                Gizmos.DrawWireCube(transform.position + melee3AreaCenter, melee3Area);
        }

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody2D>();

            hitTrigger1.Initialize(melee1AreaCenter, melee1Area, damageMelee1);
            hitTrigger2.Initialize(melee2AreaCenter, melee2Area, damageMelee2);
        }

        // Animation event
        public void AddImpulse(int power)
        {
            var dir = (flip.isFacingRight) ? 1 : -1;
            rb.AddForce(Vector2.right * dir * power, ForceMode2D.Impulse);
        }

        #region MeleeAttack1
        public override void StartAttack()
        {
            base.StartAttack();
        }

        // Animation event
        public void EnableDamageCollider1()
        {
            hitTrigger1.gameObject.SetActive(true);
        }

        // Animation event
        public void DisableDamageCollider1()
        {
            hitTrigger1.gameObject.SetActive(false);
        }
        #endregion

        #region MeleeAttack2
        public void StartMelee2()
        {
            UseAttack.Invoke();
            anim.SetTrigger("melee2");
        }

        // Animation event
        public void EableDamageCollider2()
        {
            hitTrigger2.gameObject.SetActive(true);
        }

        // Animation event
        public void DisableDamageCollider2()
        {
            hitTrigger2.gameObject.SetActive(false);
        }
        #endregion
    }
}
