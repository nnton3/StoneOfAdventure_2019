using StoneOfAdventure.UI;
using UnityEngine;
using UnityEngine.Events;

namespace StoneOfAdventure.Artifacts
{
    public class Artifact : MonoBehaviour
    {
        protected GameObject player;
        private GameObject artifactUI;
        private Animator anim;
        [HideInInspector] public UnityEvent ArtifactSelected;

        protected virtual void Start()
        {
            player = FindObjectOfType<PlayerStateController>().gameObject;
            ArtifactSelected.AddListener(() => GetComponentInParent<ArtifactSelector>().CloseArtifactSelector(gameObject));
        }

        public virtual void AddEffect()
        {
            ArtifactSelected?.Invoke();
        }

        public void Hide()
        {
            anim = GetComponent<Animator>();
            anim.SetTrigger("action");
        }
    }
}
