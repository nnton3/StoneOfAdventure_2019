using UnityEngine;
using System;

namespace StoneOfAdventure.Combat
{
    public class PlayerFighter : Fighter
    {
        private Vector3 relativePosition;
        [SerializeField] private AttackCollider attackCollider;
        [SerializeField] private float damage;

        public void Hit()
        {
            foreach (var enemie in attackCollider.EnemieList)
            {
                enemie.ApplyDamage(damage);
            }
        }
    }
}
