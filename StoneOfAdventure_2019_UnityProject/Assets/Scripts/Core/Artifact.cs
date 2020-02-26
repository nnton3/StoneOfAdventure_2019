using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class Artifact : MonoBehaviour
    {
        protected GameObject player;
        private GameObject artifactUI;
        private Animator anim;

        private bool isSelected;
        public bool IsSelected => isSelected;

        protected virtual void Start()
        {
            player = FindObjectOfType<PlayerStateController>().gameObject;
            anim = GetComponent<Animator>();
        }

        public virtual void AddEffect() { isSelected = true; }

        public void Hide()
        {
            anim.SetTrigger("action");
        }
    }
}
