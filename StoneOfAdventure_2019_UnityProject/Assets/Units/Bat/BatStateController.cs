﻿using StoneOfAdventure.Core;
using StoneOfAdventure.Movement;
using UnityEngine;

public class BatStateController : Unit
{
    private GameObject player;
    private Flyer flyer;
    private Animator anim;

    [SerializeField] private float movespeed;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        flyer = GetComponent<Flyer>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (player) Move((player.transform.position - transform.position).normalized);
    }

    private void Move(Vector2 direction)
    {
        flyer.FlyTo(direction, movespeed);
    }

    public override void Dead()
    {
        player = null;
        Move(Vector2.zero);
        anim.SetTrigger("dead");
    }

    public void DestroyUnit()
    {
        Destroy(gameObject);
    }
}
