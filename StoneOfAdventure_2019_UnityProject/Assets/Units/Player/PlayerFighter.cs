using UnityEngine;
using System;

namespace StoneOfAdventure.Combat
{
    public class PlayerFighter : Fighter
    {
        private Vector3 relativePosition;
        AttackCollider attackCollider;
        [SerializeField] private float damage;

        protected override void Start()
        {
            base.Start();
            attackCollider = GetComponentInChildren<AttackCollider>();
        }

        public void Hit()
        {
            foreach (var enemie in attackCollider.EnemieList)
            {
                enemie.ApplyDamage(damage);
            }
        }
    }
}
