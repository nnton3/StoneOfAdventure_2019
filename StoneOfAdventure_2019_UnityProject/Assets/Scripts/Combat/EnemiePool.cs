using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StoneOfAdventure.Combat
{
    public class EnemiePool : MonoBehaviour
    {
        private Dictionary<string, List<GameObject>> enemieDictionary;
        [SerializeField] private List<Pool> enemiePools;
        [SerializeField] private GameObject parentObj;

        private void Start()
        {
            enemieDictionary = new Dictionary<string, List<GameObject>>();

            foreach(var pool in enemiePools)
            {
                var enemiePool = new List<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    var obj = Instantiate(pool.pref, parentObj.transform);
                    obj.SetActive(false);
                    enemiePool.Add(obj);
                }

                enemieDictionary.Add(pool.name, enemiePool);
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

            var newInstance = Instantiate(enemieDictionary[name][0]);
            newInstance.SetActive(false);
            enemieDictionary[name].Add(newInstance);
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
