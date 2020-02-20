using UnityEngine;
using System.Collections;
using StoneOfAdventure.Combat;
using System;

namespace StoneOfAdventure.Combat
{
    public class PaladinFighter : Fighter
    {
        #region Variables
        private Rigidbody2D rb;

        [Header("MELEE ATTACK 1 SETTINGS")]
        [SerializeField] private int damageMelee1;
        [SerializeField] private Vector3 melee1AreaCenter;
        [SerializeField] private Vector3 melee1Area;
        [SerializeField] private bool melee1AreaVisible;


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
        }

        public void StartMelee2()
        {
            UseAttack.Invoke();
            anim.SetTrigger("melee2");
        }

        public void StartMelee3()
        {
            UseAttack.Invoke();
            anim.SetTrigger("melee3");
        }

        // Animation event
        public void MeleeAttack1Hit()
        {
            Hit(damageMelee1, melee1AreaCenter, melee1Area);
        }

        // Animation event
        public void MeleeAttack2Hit()
        {
            Hit(damageMelee2, melee2AreaCenter, melee2Area);
        }

        // Animation event
        public void MeleeAttack3Hit()
        {
            Hit(damageMelee3, melee3AreaCenter, melee3Area);
        }

        public void Hit(int baseDamage, Vector3 applicationAreaCenter, Vector3 applicationArea)
        {
            var currentDamage = baseDamage;
            applyDamageModifiers?.Invoke(ref currentDamage);
            Vector2 centerInRelationUnitDirection =
                transform.position + applicationAreaCenter * ((flip.isFacingRight) ? 1 : -1);
            var player = Physics2D.OverlapBox(
                centerInRelationUnitDirection,
                applicationArea,
                0f,
                layerMask);

            if (player != null)
            {
                player.GetComponent<Health>().ApplyDamage(currentDamage);
                applyEffectsOnTarget?.Invoke(player.gameObject);
                DamageApplied?.Invoke();
            }
        }
    }
}
