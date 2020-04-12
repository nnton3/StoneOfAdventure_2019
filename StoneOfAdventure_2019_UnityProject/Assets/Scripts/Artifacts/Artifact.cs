using StoneOfAdventure.UI;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

namespace StoneOfAdventure.Artifacts
{
    public class Artifact : MonoBehaviour
    {
        private GameObject artifactUI;
        private Fader fader;
        private CanvasGroup canvasGroup;

        [Inject (Id = "Player")] protected PlayerStateController player;
        [Inject] protected ArtifactsController artifactsController;
        [Inject] protected DiContainer Container;
        [Inject] private SignalBus signalBus;

        protected virtual void Start()
        {
            fader = GetComponent<Fader>();
            canvasGroup = GetComponent<CanvasGroup>();
            GetComponent<Button>().onClick.AddListener(() => signalBus.Fire(new ArtifactSelected { art = gameObject }));            
        }

        public virtual void AddEffect()
        {
            artifactsController.AddArt(this);
        }

        public void Hide()
        {
            fader.Hide();
        }

        protected float CalculateNewChanceValue(float baseChance, float denominatorOfProgression)
        {
            var addedChance = baseChance * Mathf.Pow(denominatorOfProgression, artifactsController.GetArtLvl(this) - 1);
            return (baseChance - addedChance * denominatorOfProgression) / (1 - denominatorOfProgression);
        }
    }
}
