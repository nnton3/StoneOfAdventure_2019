using System.Collections.Generic;
using UnityEngine;
using StoneOfAdventure.Artifacts;
using UnityEngine.UI;
using System.Collections;

namespace StoneOfAdventure.UI
{
    public class ArtifactSelector : MonoBehaviour
    {
        [SerializeField] private List<GameObject> selectedArtifacts;
        [SerializeField] private GameObject bck;
        private Fader fader;
        private ArtifactsPool artsPool;
        private CanvasGroup canvasGroup;

        private void Start()
        {
            fader = bck.GetComponentInChildren<Fader>();
            artsPool = GetComponentInChildren<ArtifactsPool>();
            canvasGroup = GetComponentInChildren<CanvasGroup>();
        }

        public void EnableArtifactSelector()
        {
            Time.timeScale = 0f;
            canvasGroup.interactable = true;
            fader.Show();
            ShowArtifacts();
        }

        private void ShowArtifacts()
        {
            var arts = artsPool.GetArt();
            for (int i = 0; i < arts.Count; i++)
            {
                selectedArtifacts.Add(arts[i]);
                selectedArtifacts[i].GetComponent<Button>().onClick.AddListener(() => StartCoroutine("Hide"));
            }
        }

        private IEnumerator Hide()
        {
            var arts = GetComponentsInChildren<Artifact>();
            HideNotSelectedArts(arts);
            yield return new WaitForSecondsRealtime(1f);
            fader.Hide();
            yield return new WaitForSecondsRealtime(1f);
            ClearSelector(arts);
            Time.timeScale = 1f;
        }

        private void ClearSelector(Artifact[] arts)
        {
            for (int i = 0; i < arts.Length; i++)
            {
                
                arts[i].gameObject.SetActive(false);
            }
            selectedArtifacts.RemoveAll((isArt) => true);
            canvasGroup.interactable = false;
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
