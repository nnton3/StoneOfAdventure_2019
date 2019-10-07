using UnityEngine;
using UnityEngine.Events;

namespace StoneOfAdventure.Combat
{
    public class EnemyDetector : MonoBehaviour
    {
        public UnityEvent PlayerDetected;
        public UnityEvent PlayerLost;
        private GameObject player;
        public GameObject Player => player;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                player = collision.gameObject;
                PlayerDetected.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                player = null;
                PlayerLost.Invoke();
            }
        }
    }
}
