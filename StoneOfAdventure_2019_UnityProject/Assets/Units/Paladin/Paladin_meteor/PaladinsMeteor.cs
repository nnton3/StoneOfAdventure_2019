using UnityEngine;

namespace StoneOfAdventure.Combat
{
    public class PaladinsMeteor : MonoBehaviour
    {
        [SerializeField] private int damage;

        // Animation event
        public void ApplyDamage()
        {
            Vector2 centerInRelationUnitDirection =
                   transform.position + applicationAreaCenter;
            var player = Physics2D.OverlapBox(
                centerInRelationUnitDirection,
                applicationArea,
                0f,
                layerMask);
        
            if (player != null) player.GetComponent<Health>().ApplyDamage(damage);
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
