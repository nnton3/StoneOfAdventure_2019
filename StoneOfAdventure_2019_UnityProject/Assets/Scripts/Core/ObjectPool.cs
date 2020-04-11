using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace StoneOfAdventure.Core
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject targetPref;
        [SerializeField] private string parentObjName = "Pool";
        [SerializeField] private int size;

        [Inject] private DiContainer container;

        private List<GameObject> objectList = new List<GameObject>();
        private Transform parentObj;

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

            parentObj = Instantiate(new GameObject(parentObjName)).transform;

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
            var newObj = container.InstantiatePrefab(targetPref, parentObj);
            objectList.Add(newObj);
            return newObj;
        }
    }
}
