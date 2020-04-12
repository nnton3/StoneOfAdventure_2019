using UnityEngine;
using System.Collections.Generic;

namespace StoneOfAdventure.Artifacts
{
    public class ArtifactsPool : MonoBehaviour
    {
        private List<GameObject> artsInPool = new List<GameObject>();
        private List<GameObject> notUsableArts = new List<GameObject>();
        private List<GameObject> selectedArts = new List<GameObject>();

        private void Awake()
        {
            foreach (var art in GetComponentsInChildren<Artifact>())
            {
                artsInPool.Add(art.gameObject);
                art.gameObject.SetActive(false);
            }
        }

        public List<GameObject> GetArt()
        {
            for (int i = 0; i < 3; i++)
            {
                var selectedArt = artsInPool[Random.Range(0, notUsableArts.Count - 1)];
                artsInPool.Remove(selectedArt);
                selectedArt.SetActive(true);
                selectedArts.Add(selectedArt);
            }

            return selectedArts;
        }
    }
}
