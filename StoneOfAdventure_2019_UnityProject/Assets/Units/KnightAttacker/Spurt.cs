using UnityEngine;
using System.Collections;
using StoneOfAdventure.Movement;

namespace StoneOfAdventure.Combat
{
    public class Spurt : MonoBehaviour
    {
        private Animator anim;

        [SerializeField] private float emergenceDistance = 1f;
        private GameObject player;

        private void Start()
        {
            anim = GetComponent<Animator>();
            player = FindObjectOfType<PlayerStateController>().gameObject;
        }

        public void StartAction()
        {
            anim.SetTrigger("spurt");
        }

        public void TeleportBehindTheBack()
        {
            float direction = (player.GetComponent<Flip>().isFacingRight) ? 1 : -1;
            Vector3 emergencePosition = new Vector3(
                player.transform.position.x - emergenceDistance * direction,
                player.transform.position.y,
                player.transform.position.z);
            transform.position = emergencePosition;
        }
    }
}
