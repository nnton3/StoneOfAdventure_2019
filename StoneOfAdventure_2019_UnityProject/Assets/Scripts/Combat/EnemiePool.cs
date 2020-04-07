using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace StoneOfAdventure.Combat
{
    public class EnemiePool : MonoBehaviour
    {
        [Inject] private DiContainer container;

        [SerializeField] private List<Pool> enemiePools;

        private Dictionary<string, List<GameObject>> enemieDictionary;
        private GameObject parentObj;

        private void Start()
        {
            enemieDictionary = new Dictionary<string, List<GameObject>>();

            SetParentObj();

            foreach (var pool in enemiePools)
            {
                var enemiePool = new List<GameObject>();


                for (int i = 0; i < pool.size; i++)
                {
                    var obj = container.InstantiatePrefab(pool.pref, parentObj.transform);
                    enemiePool.Add(obj);
                    obj.SetActive(false);
                }

                enemieDictionary.Add(pool.name, enemiePool);
            }
        }

        private void SetParentObj()
        {
            parentObj = GameObject.Find("EnemiesPool");
            if (parentObj == null)
            {
                parentObj = Instantiate(new GameObject("EnemiesPool"));
            }
        }

        public GameObject GetEnemie(string name)
        {
            for (int i = 0; i < enemieDictionary[name].Count; i++)
            {
                if (!enemieDictionary[name][i].activeSelf)
                {
                    return enemieDictionary[name][i];
                }
            }

            var newInstance = container.InstantiatePrefab(enemieDictionary[name][0], parentObj.transform);
            enemieDictionary[name].Add(newInstance);
            newInstance.SetActive(false);
            return enemieDictionary[name][enemieDictionary[name].Count - 1];
        }

        [System.Serializable]
        private struct Pool
        {
            public string name;
            public GameObject pref;
            public int size;
        }
    }
}
