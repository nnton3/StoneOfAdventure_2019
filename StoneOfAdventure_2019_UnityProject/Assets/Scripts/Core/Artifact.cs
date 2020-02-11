using UnityEngine;
using UnityEngine.Events;

namespace StoneOfAdventure.Artifacts
{
    public class Artifact : MonoBehaviour
    {
        protected GameObject player;
        private GameObject artifactUI;
        private Animator anim;
        public UnityEvent ArtifactSelected;

        protected virtual void Start()
        {
            Debug.Log($"anim = null? {anim == null}");
            player = FindObjectOfType<PlayerStateController>().gameObject;
        }

        public virtual void AddEffect()
        {
            ArtifactSelected?.Invoke();
        }

        public void Hide()
        {
            Debug.Log("work");
            Debug.Log(anim == null);
            anim = GetComponent<Animator>();
            anim.SetTrigger("action");
            Debug.Log("not work");
        }
    }
}
