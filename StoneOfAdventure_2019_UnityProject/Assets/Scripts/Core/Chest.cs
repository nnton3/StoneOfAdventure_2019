using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private Treasury treasury;
    [SerializeField] private int price = 10;
    [SerializeField] private List<Artifact> artifactList = new List<Artifact>();
    private GameObject chestUI;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        treasury = FindObjectOfType<Treasury>();
        chestUI = GetComponentInChildren<Canvas>().gameObject;
    }

    // Animation event
    public void TryOpenChest()
    {
        if (price > treasury.CurrentSoulsPoints) return;
        anim.SetTrigger("use");
    }

    public void GiveReward()
    {
        treasury.Spend(price);
        Instantiate(artifactList[Random.Range(0, artifactList.Count)], transform.position, Quaternion.identity);
        Destroy(chestUI);
    }
}
