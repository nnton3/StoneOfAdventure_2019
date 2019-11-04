using UnityEngine;
using System.Collections;

public class Artifact : MonoBehaviour
{
    protected GameObject player;

    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerStateController>().gameObject;
    }

}
