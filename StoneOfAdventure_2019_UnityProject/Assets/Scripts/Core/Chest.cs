using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Treasury treasury;
    [SerializeField] private int price = 10;
    [SerializeField] private List<Artifact> artifactList = new List<Artifact>();
    private GameObject chestUI;

    private void Start()
    {
        treasury = FindObjectOfType<Treasury>();
        chestUI = GetComponentInChildren<Canvas>().gameObject;
    }

    public void TryOpenChest()
    {
        if (price > treasury.CurrentSoulsPoints) return;
        treasury.Spend(price);
        Instantiate(artifactList[Random.Range(0, artifactList.Count)], transform.position, Quaternion.identity);
        Destroy(chestUI);
    }
}
