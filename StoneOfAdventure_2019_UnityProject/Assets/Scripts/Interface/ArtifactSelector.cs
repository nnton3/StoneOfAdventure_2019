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
        [SerializeField] private GameObject[] artifacts;
        [SerializeField] private List<GameObject> selectedArtifacts;
        private Animator anim;
        [SerializeField] private GameObject bck;

        private void Start()
        {
            anim = bck.GetComponentInChildren<Animator>();
        }

        public void EnableArtifactSelector()
        {
            Time.timeScale = 0f;
            anim.SetTrigger("action");
            ShowArtifacts(SelectArtifacts());
        }

        private void ShowArtifacts(List<GameObject> prefs)
        {
            for (int i = 0; i < prefs.Count; i++)
            {
                var artifact = Instantiate(prefs[i], bck.transform);
                selectedArtifacts[i] = artifact;
                selectedArtifacts[i].GetComponent<Button>().onClick.AddListener(CloseArtifactSelector);
            }
        }

        public void CloseArtifactSelector()
        {
            Hide();
        }

        async void Hide()
        {
            var arts = GetComponentsInChildren<Artifact>();
            HideNotSelectedArts(arts);
            await Task.Delay(TimeSpan.FromSeconds(1));
            anim.SetTrigger("action");
            await Task.Delay(TimeSpan.FromSeconds(1));
            ClearSelector(arts);
            Time.timeScale = 1f;
        }

        private static void HideNotSelectedArts(Artifact[] arts)
        {
            for (int j = 0; j < arts.Length; j++)
            {
                Debug.Log(arts[j].IsSelected);
                if (!arts[j].IsSelected) arts[j].Hide();
            }
        }

        private static void ClearSelector(Artifact[] arts)
        {
            for (int j = 0; j < arts.Length; j++)
            {
                Destroy(arts[j].gameObject);
            }
        }

        private List<GameObject> SelectArtifacts()
        {
            selectedArtifacts = new List<GameObject>()
            {
                artifacts[UnityEngine.Random.Range(0, artifacts.Length)],
                artifacts[UnityEngine.Random.Range(0, artifacts.Length)],
                artifacts[UnityEngine.Random.Range(0, artifacts.Length)]
            };

            return selectedArtifacts;
        }
    }
}
