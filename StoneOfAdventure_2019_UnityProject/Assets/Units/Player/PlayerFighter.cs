using UnityEngine;
using System;

namespace StoneOfAdventure.Combat
{
    public class PlayerFighter : Fighter
    {
        private Vector3 relativePosition;
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

        internal void Hit()
        {
            float currentDamage = damage * damageScale;
            Collider[] enemiesInApplicationArea = Physics.OverlapBox(transform.position + applicationAreaCenter, applicationArea / 2, Quaternion.identity);
            foreach (var enemie in enemiesInApplicationArea)
            {
                Debug.Log(enemie.name);
                enemie.GetComponent<Health>().ApplyDamage(currentDamage);
            }
            ResetDamageScale();
        }

        [SerializeField] private Vector3 applicationAreaCenter;
        [SerializeField] private Vector3 applicationArea;
        [SerializeField] private bool applicationAreaVisible;
        [SerializeField] private LayerMask layerMask;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (applicationAreaVisible)
                Gizmos.DrawWireCube(transform.position + applicationAreaCenter, applicationArea);
        }
    }
}
