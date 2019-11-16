using UnityEngine;
using System;

namespace StoneOfAdventure.Combat
{
    public class PlayerFighter : Fighter
    {
        [SerializeField] private float damage;
        private float damageScale = 1f;
        

        public void SetDamageScaleForNexAttack(float _damageScale)
        {
            if (_damageScale > 0f) damageScale = _damageScale;
        }

        private void ResetDamageScale() { damageScale = 1f; }

        // Animation event
        public void Hit()
        {
            float currentDamage = damage * damageScale;
            Vector2 centerInRelationUnitDirection =
                transform.position + applicationAreaCenter * ((flip.isFacingRight) ? 1 : -1);
            Collider2D[] enemiesInApplicationArea = Physics2D.OverlapBoxAll(
                centerInRelationUnitDirection,
                applicationArea,
                0f,
                layerMask);
            foreach (var enemie in enemiesInApplicationArea)
            {
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
