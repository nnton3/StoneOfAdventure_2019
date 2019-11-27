using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Artifact : MonoBehaviour
{
    protected GameObject player;
    private GameObject artifactUI;

    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerStateController>().gameObject;
        artifactUI = GameObject.Find("BuffsGroup");
    }
    
    protected void AddArtifactOnCanvas()
    {
        var uiInstance = Instantiate(new GameObject(), artifactUI.transform);
        var image = uiInstance.AddComponent<Image>();
        image.sprite = GetComponent<SpriteRenderer>().sprite;
    }
}
