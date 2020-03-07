using StoneOfAdventure.UI;
using UnityEngine;

namespace StoneOfAdventure.Artifacts
{
    public class Artifact : MonoBehaviour
    {
        protected GameObject player;
        private GameObject artifactUI;
        private Fader fader;

        private bool isSelected;
        public bool IsSelected => isSelected;

        protected virtual void Start()
        {
            player = FindObjectOfType<PlayerStateController>().gameObject;
            fader = GetComponent<Fader>();
        }

        public virtual void AddEffect() { isSelected = true; }

        public void Hide()
        {
            fader.StartCoroutine("Hide");
        }
    }
}
