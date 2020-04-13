using System.Collections.Generic;
using UnityEngine;
using StoneOfAdventure.Artifacts;
using Zenject;

namespace StoneOfAdventure.UI
{
    public class ArtifactSelector : MonoBehaviour
    {
        [SerializeField] private List<GameObject> selectedArtifacts = new List<GameObject>();
        [SerializeField] private GameObject bck;

        private Fader fader;
        private ArtifactsPool artsPool;
        private CanvasGroup canvasGroup;

        [Inject] private SignalBus signalBus;

        private void Start()
        {
            fader = bck.GetComponentInChildren<Fader>();
            artsPool = GetComponentInChildren<ArtifactsPool>();
            canvasGroup = GetComponentInChildren<CanvasGroup>();

            signalBus.Subscribe<ArtifactSelected>(CloseSelector);
        }

        public void OpenArtifactSelector()
        {
            Time.timeScale = 0f;
            canvasGroup.interactable = true;
            FillSelector();
            fader.Show();
        }

        private void CloseSelector(ArtifactSelected args)
        {
            fader.Hide();
            Time.timeScale = 1f;
            canvasGroup.interactable = false;
        }

        private void FillSelector()
        {
                for (int i = 0; i < selectedArtifacts.Count; i++)
            {
                selectedArtifacts[i].gameObject.SetActive(false);
            }

            selectedArtifacts = artsPool.GetArt();

            for (int i = 0; i < selectedArtifacts.Count; i++)
            {
                selectedArtifacts[i].gameObject.SetActive(true);
            }
        }
    }
}
