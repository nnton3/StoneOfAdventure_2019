using UnityEngine;
using System.Collections.Generic;

namespace StoneOfAdventure.Artifacts
{
    public class ArtifactsPool : MonoBehaviour
    {
        private List<GameObject> artsInPool = new List<GameObject>();
        private List<GameObject> notUsableArts = new List<GameObject>();
        private List<GameObject> selectedArts = new List<GameObject>();

        private void Start()
        {
            var arts = GetComponentsInChildren<Artifact>();
            foreach (var art in arts)
            {
                artsInPool.Add(art.gameObject);
                art.gameObject.SetActive(false);
            }
        }

        public List<GameObject> GetArt()
        {
            notUsableArts = artsInPool;

            for (int i = 0; i < 3; i++)
            {
                var selectedArt = notUsableArts[Random.Range(0, notUsableArts.Count - 1)];
                notUsableArts.Remove(selectedArt);
                selectedArt.SetActive(true);
                selectedArts.Add(selectedArt);
            }

            return selectedArts;
        }
    }
}
