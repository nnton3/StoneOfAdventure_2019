﻿using StoneOfAdventure.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedColorIndication : MonoBehaviour
{
    private Health health;
    private Animator anim;

    private void Start()
    {
        health = GetComponent<Health>();
        anim = GetComponent<Animator>();

        health.HPDecreased.AddListener(SwapColor);
    }

    private void SwapColor()
    {
        anim.SetTrigger("damaged");
    }
}
