using UnityEngine;

namespace StoneOfAdventure.Combat
{
    public class RaycastFighter : Fighter
    {
        #region Variables
        [SerializeField] private float attackRange;
        [SerializeField] private LayerMask layerMask;
        #endregion

        // Animation event
        public void Hit()
        {
            Vector2 attackDirection = Vector2.right * ((flip.isFacingRight) ? 1 : -1);
            Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y + 0.5f);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, attackDirection, attackRange, layerMask);
            
            if (hit)
            {
                Debug.Log("hit");
                var target = hit.transform.gameObject;
                var currentDamage = baseDamage;
                applyDamageModifiers?.Invoke(ref currentDamage);
                target.GetComponent<Health>().ApplyDamage(currentDamage);
                applyEffectsOnTarget?.Invoke(target);
                DamageApplied?.Invoke();
            }
        }
    }
}
