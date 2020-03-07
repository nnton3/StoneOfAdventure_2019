using UnityEngine;
using UnityEngine.Events;

namespace StoneOfAdventure.Combat
{
    public class EnemyDetector : MonoBehaviour
    {
        [HideInInspector] public UnityEvent PlayerDetected;
        [HideInInspector] public UnityEvent PlayerLost;
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

        private void OnDestroy()
        {
            PlayerDetected.RemoveAllListeners();
            PlayerLost.RemoveAllListeners();
        }
    }
}
