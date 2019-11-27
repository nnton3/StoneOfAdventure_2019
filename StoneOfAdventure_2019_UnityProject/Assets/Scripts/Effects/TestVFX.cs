using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class TestVFX : MonoBehaviour
{
    private VisualEffect visualEffect;

    private void Start()
    {
        visualEffect = GetComponent<VisualEffect>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            DisableVFX();
        }
    }

    private void DisableVFX()
    {

    }
}
