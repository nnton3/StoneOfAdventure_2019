﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using StoneOfAdventure.Core;
using StoneOfAdventure.Combat;

public class BuffArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemie"))
        {
            AddBuffs(collision);
        }
    }

    protected virtual void AddBuffs(Collider2D collision)
    {
        //collision.gameObject.;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemie"))
        {
            RemoveBuffs(collision);
        }
    }

    protected virtual void RemoveBuffs(Collider2D collision) { }
}