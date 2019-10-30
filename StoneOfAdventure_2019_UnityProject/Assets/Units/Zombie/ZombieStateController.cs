﻿using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;

public class ZombieStateController : Unit
{
    private Flip flip;
    private EnemyDetector enemyDetector;
    private object target;
    private GameObject player;
    public IZombieState State;
    private ZombieIdleState idleState;

    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float movespeed = 3f;

    [SerializeField] private string currentState = "";

    private void Start()
    {
        enemyDetector = GetComponentInChildren<EnemyDetector>();
        flip = GetComponent<Flip>();

        idleState = GetComponent<ZombieIdleState>();

        DisableState();
        
        enemyDetector.PlayerDetected.AddListener(() => UpdateTarget());
        enemyDetector.PlayerLost.AddListener(() => UpdateTarget());
    }

    private void Update()
    {
        if (player)
        {
            if (PlayerInAttackRange())
            {
                if (PlayerInFront()) Attack();
            }
        }
        else DisableState();

        MoveHorizontal(CalculateDirection(), movespeed);

        currentState = State.ToString();
    }

    private bool PlayerInFront()
    {
        if (flip.isFacingRight && CalculateDirection() == 1f ||
            !flip.isFacingRight && CalculateDirection() == -1f)
             return true;
        else return false;
    }

    private float CalculateDirection()
    {
        if (player)
        {
            return Mathf.Sign(player.transform.position.x - transform.position.x);
        }
        else return 0f;
    }

    private bool PlayerInAttackRange()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x) <= attackRange;
    }

    public void UpdateTarget() { player = enemyDetector.Player; }

    private void Attack() { State.Attack(); }

    private void MoveHorizontal(float direction, float movespeed) { State.MoveHorizontal(direction, movespeed); }

    public override void DisableState() { State = idleState; }

    public override void Dead()
    {
        State.Dead();
        enemyDetector.enabled = false;
    }

    public override void ApplyStun(float timeOfStun)
    {
        State.Stun(timeOfStun);
    }
}