using UnityEngine;
using System;

namespace StoneOfAdventure.Combat
{
    public class PlayerFighter : Fighter
    {
        private Vector3 relativePosition;
        [SerializeField] private AttackCollider attackCollider;
        [SerializeField] private float damage;
        private float damageScale = 1f;
        private float currentAttackSpeed = 1f;

        public void ModifyAttackSpeed(float addedAttackspeedInPercent)
        {
            currentAttackSpeed += addedAttackspeedInPercent;
            anim.SetFloat("currentAttackSpeed", currentAttackSpeed);
        }

        public void SetDamageScaleForNexAttack(float _damageScale)
        {
            if (_damageScale > 0f) damageScale = _damageScale;
        }

        private void ResetDamageScale() { damageScale = 1f; }

        public void Hit()
        {
            float currentDamage = damage * damageScale;
            foreach (var enemie in attackCollider.EnemieList)
            {
                enemie.ApplyDamage(currentDamage);
            }
            ResetDamageScale();
        }
    }
}
