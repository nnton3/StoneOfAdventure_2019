﻿using UnityEngine;
using System.Collections;

public class DysableTimer : MonoBehaviour
{
    [SerializeField] private float lifetime = 0.5f;

    private IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }
}
