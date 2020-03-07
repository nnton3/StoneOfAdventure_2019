using UnityEngine;

namespace StoneOfAdventure.Combat
{
    public class OneHitTrigger : MonoBehaviour
    {
        private int damage = 5;
        [SerializeField] private string targetTag = "Enemie";

        public void Initialize(Vector3 center, Vector3 size, int damage)
        {
            var collider = GetComponent<BoxCollider2D>();
            collider.offset = center;
            collider.size = size;
            this.damage = damage;
            gameObject.SetActive(false);
        }

        public void Initialize(int damage)
        {
            this.damage = damage;
            gameObject.SetActive(false);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(targetTag))
            {
                ApplyDamage(collision);
            }
        }

        protected virtual void ApplyDamage(Collider2D collision)
        {
            collision.GetComponent<Health>().ApplyDamage(damage);
        }

    }
}
