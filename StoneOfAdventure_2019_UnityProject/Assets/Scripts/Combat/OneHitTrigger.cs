using UnityEngine;

namespace StoneOfAdventure.Combat
{
    public class OneHitTrigger : MonoBehaviour
    {
        private int damage = 5;
        [SerializeField] private string targetTag = "Enemie";

        public void Initialize(Vector3 center, Vector3 size, int damage)
        {
            var collider = GetComponent<BoxCollider>();
            collider.center = center;
            collider.size = size;
            damage = this.damage;
        }

        public void Initialize(int damage)
        {
            this.damage = damage;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(targetTag))
            {
                ApplyDamage(collision);
            }
        }

        protected void ApplyDamage(Collider2D collision)
        {
            Debug.Log(collision.name);
            Debug.Log(collision.tag);
            collision.GetComponent<Health>().ApplyDamage(damage);
        }

    }
}
