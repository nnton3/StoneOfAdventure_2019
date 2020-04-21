using System;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using StoneOfAdventure.Movement;
using UnityEngine;
using Zenject;

public class BatStateController : UnitContainsAward
{
    [Inject (Id = "Player")] private PlayerStateController player;
    [Inject] DiContainer container;
    private Flyer flyer;
    private Animator anim;
    private Health health;

    [SerializeField] private float movespeed;

    private void OnEnable()
    {
        if (health == null) Initialize();

        if (health.Untouchable) health.SwapUntouchable();

        Debug.Log($"My postion: {transform.position}");
        Debug.Log($"Enemie position {player.transform.position}");
        transform.position = player.transform.position + Vector3.left * 20f;
        Debug.Log($"My new position {transform.position}");
        Debug.Log($"Have I target {player != null}");
    }

    private void Initialize()
    {
        container.Inject(this);
        health = GetComponent<Health>();
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
        Move(Vector2.zero);
        anim.SetTrigger("dead");
        CreateReward();
    }

    // Animation event
    public void DisableObject()
    {
        ReturnToPool();
    }
}
