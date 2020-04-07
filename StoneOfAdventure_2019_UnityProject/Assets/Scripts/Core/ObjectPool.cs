using System.Collections.Generic;
using UnityEngine;

namespace StoneOfAdventure.Core
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject targetPref;
        [SerializeField] private string parentObjName = "Pool";
        [SerializeField] private int size;

        private List<GameObject> objectList = new List<GameObject>();
        private GameObject parentObj;

        private void Start()
        {
            FillPool(size);
        }

        public void FillPool(int value)
        {
            if (targetPref == null)
            {
                Debug.Log($"Pref in pool {parentObjName} not found");
                return;
            }

            parentObj = GameObject.Find(parentObjName);
            if (parentObj == null)
                parentObj = Instantiate(new GameObject(parentObjName));

            for (int i = 0; i < value; i++)
            {
                var newObj = CreateNewObject();
                newObj.SetActive(false);
            }
        }

        public GameObject GetObject()
        {
            for (int i = 0; i < objectList.Count; i++)
            {
                if (!objectList[i].activeSelf) return objectList[i];
            }
            return CreateNewObject();
        }

        private GameObject CreateNewObject()
        {
            var newObj = Instantiate(targetPref, parentObj.transform);
            objectList.Add(newObj);
            return newObj;
        }
    }
}
