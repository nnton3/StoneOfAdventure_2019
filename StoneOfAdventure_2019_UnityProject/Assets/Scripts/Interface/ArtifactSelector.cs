using UnityEngine;

namespace StoneOfAdventure.UI
{
    public class ArtifactSelector : MonoBehaviour
    {
        [SerializeField] private GameObject[] artifacts;

        public void EnableArtifactSelector()
        {
            gameObject.SetActive(true);
            ShowArtifacts(SelectArtifacts());
        }

        private void ShowArtifacts(GameObject[] prefs)
        {
            for (int i = 0; i < prefs.Length; i++)
            {
                Instantiate(prefs[i], transform);
            }
        }

        private GameObject[] SelectArtifacts()
        {
            return new GameObject[]
            {
                artifacts[Random.Range(0, artifacts.Length - 1)],
                artifacts[Random.Range(0, artifacts.Length - 1)],
                artifacts[Random.Range(0, artifacts.Length - 1)]
            };
            
        }
    }
}
