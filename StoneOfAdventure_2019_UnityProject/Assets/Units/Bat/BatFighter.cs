using UnityEngine;

namespace StoneOfAdventure.Combat
{
    public class BatFighter : Fighter
    {
        private Health playerHealth;
        [SerializeField] private float damage = 1f;

        private void OnTriggerEnter2D(Collider2D enemie)
        {
            if (enemie.CompareTag("Player"))
            {
                playerHealth = enemie.GetComponent<Health>();
                InvokeRepeating("Hit", 1f, 1f);
            }
        }

        private void OnTriggerExit2D(Collider2D enemie)
        {
            if (enemie.CompareTag("Player")) { CancelAttack(); }
        }
        
        private void Hit() { playerHealth.ApplyDamage(damage); }

        public override void CancelAttack() { CancelInvoke("Hit"); }
    }
}
