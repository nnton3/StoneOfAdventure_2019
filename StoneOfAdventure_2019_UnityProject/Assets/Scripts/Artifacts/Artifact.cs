using StoneOfAdventure.UI;
using UnityEngine;
using Zenject;

namespace StoneOfAdventure.Artifacts
{
    public class Artifact : MonoBehaviour
    {
        private GameObject artifactUI;
        private Fader fader;
        [Inject (Id = "Player")] protected PlayerStateController player;
        [Inject] protected ArtifactsController artifactsController;
        [Inject] protected DiContainer Container;

        private bool isSelected;
        public bool IsSelected => isSelected;

        protected virtual void Start()
        {
            fader = GetComponent<Fader>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                AddEffect();
            }
        }

        public virtual void AddEffect()
        {
            artifactsController.AddArt(this);
            isSelected = true;
        }

        public void Hide()
        {
            fader.StartCoroutine("Hide");
        }

        protected float CalculateNewChanceValue(float baseChance, float denominatorOfProgression)
        {
            var addedChance = baseChance * Mathf.Pow(denominatorOfProgression, artifactsController.GetArtLvl(this) - 1);
            return (baseChance - addedChance * denominatorOfProgression) / (1 - denominatorOfProgression);
        }
    }
}
