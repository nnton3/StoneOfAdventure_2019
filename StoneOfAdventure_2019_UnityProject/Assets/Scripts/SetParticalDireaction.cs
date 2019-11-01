using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class SetParticalDireaction : MonoBehaviour
{
    [SerializeField] private VisualEffect visualEffect;

    private void Start()
    {
        visualEffect = GetComponent<VisualEffect>();

        Debug.Log(visualEffect);
    }
}
