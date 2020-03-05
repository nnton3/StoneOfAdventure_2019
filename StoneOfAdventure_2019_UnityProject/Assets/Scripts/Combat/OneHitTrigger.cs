using UnityEngine;

namespace StoneOfAdventure.Combat
{
    public class OneHitTrigger : MonoBehaviour
    {
        private int damage = 5;

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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemie") || collision.CompareTag("Boss"))
                collision.GetComponent<Health>().ApplyDamage(damage);
        }
    }
}
