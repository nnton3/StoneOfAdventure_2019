using UnityEngine;
using UnityEngine.Events;

namespace StoneOfAdventure.Combat
{
    public class AOE_Fighter : Fighter
    {
        // Animation event
        public void Hit()
        {
            var currentDamage = baseDamage;
            applyDamageModifiers?.Invoke(ref currentDamage);
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
                applyEffectsOnTarget?.Invoke(enemie.gameObject);
                DamageApplied?.Invoke();
            }
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
