using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoneOfAdventure.Artifacts
{
    public class ArtifactsPool : MonoBehaviour
    {
        private List<GameObject> artsInPool = new List<GameObject>();
        private List<GameObject> selectedArts = new List<GameObject>();
        private System.Random random = new System.Random();

        private void Awake()
        {
            foreach (var art in GetComponentsInChildren<Artifact>())
            {
                artsInPool.Add(art.gameObject);
                art.gameObject.SetActive(false);
            }
        }

        private void Shuffle (ref List<GameObject> list)
        {
            for (int i = list.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);

                var tmp = list[j];
                list[j] = list[i];
                list[i] = tmp;
            }
        }

        public List<GameObject> GetArt()
        {
            selectedArts.Clear();
            Shuffle(ref artsInPool);

            for (int i = 0; i < 3; i++)
            {
                var selectedArt = artsInPool[i];
                selectedArts.Add(selectedArt);
            }

            return selectedArts;
        }
    }
}
