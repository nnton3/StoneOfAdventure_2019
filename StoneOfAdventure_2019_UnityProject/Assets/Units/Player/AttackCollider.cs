using UnityEngine;
using System.Collections.Generic;

namespace StoneOfAdventure.Combat
{
    public class AttackCollider : MonoBehaviour
    {
        [SerializeField] private List<Health> enemiesList = new List<Health>();
        public List<Health> EnemieList => enemiesList;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemie"))
            {
                enemiesList.Add(collision.GetComponent<Health>());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemie"))
            {
                enemiesList.Remove(collision.GetComponent<Health>());
            }
        }
    }
}
