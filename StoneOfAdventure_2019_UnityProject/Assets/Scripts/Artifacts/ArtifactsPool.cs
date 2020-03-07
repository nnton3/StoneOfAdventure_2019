using UnityEngine;
using System.Collections.Generic;

namespace StoneOfAdventure.Artifacts
{
    public class ArtifactsPool : MonoBehaviour
    {
        private List<GameObject> artsInPool = new List<GameObject>();

        private void Awake()
        {
            var arts = GetComponentsInChildren<Artifact>();
            foreach (var art in arts)
            {
                artsInPool.Add(art.gameObject);
                art.gameObject.SetActive(false);
            }
        }

        public GameObject GetArt()
        {
            int i = Random.Range(0, artsInPool.Count - 1);
            while (artsInPool[i].activeSelf)
            {
                Debug.Log("get next art");
                i = Random.Range(0, artsInPool.Count - 1);
            }

            artsInPool[i].SetActive(true);
            return artsInPool[i];
        }
    }
}
