using System.Collections.Generic;
using UnityEngine;
using StoneOfAdventure.Artifacts;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;

namespace StoneOfAdventure.UI
{
    public class ArtifactSelector : MonoBehaviour
    {
        [SerializeField] private List<GameObject> selectedArtifacts;
        private Fader fader;
        private ArtifactsPool artsPool;
        [SerializeField] private GameObject bck;
        private CanvasGroup canvasGroup;

        private void Start()
        {
            fader = bck.GetComponentInChildren<Fader>();
            artsPool = GetComponentInChildren<ArtifactsPool>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void EnableArtifactSelector()
        {
            Time.timeScale = 0f;
            canvasGroup.interactable = true;
            fader.StartCoroutine("Show");
            ShowArtifacts();
        }

        private void ShowArtifacts()
        {
            for (int i = 0; i < 3; i++)
            {
                selectedArtifacts.Add(artsPool.GetArt());
                selectedArtifacts[i].GetComponent<Button>().onClick.AddListener(Hide);
            }
        }

        async void Hide()
        {
            var arts = GetComponentsInChildren<Artifact>();
            HideNotSelectedArts(arts);
            await Task.Delay(TimeSpan.FromSeconds(1));
            fader.StartCoroutine("Hide");
            await Task.Delay(TimeSpan.FromSeconds(1));
            ClearSelector(arts);
            Time.timeScale = 1f;
        }

        private void ClearSelector(Artifact[] arts)
        {
            for (int i = 0; i < arts.Length; i++)
            {
                
                arts[i].gameObject.SetActive(false);
            }
            selectedArtifacts.RemoveAll(IsArtifact);
            canvasGroup.interactable = false;
        }

        private bool IsArtifact(GameObject obj)
        {
            return true;
        }

        private static void HideNotSelectedArts(Artifact[] arts)
        {
            for (int j = 0; j < arts.Length; j++)
            {
                if (!arts[j].IsSelected) arts[j].Hide();
            }
        }
    }
}
