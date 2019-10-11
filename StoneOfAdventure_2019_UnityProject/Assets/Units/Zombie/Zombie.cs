using System;
using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Movement;
using StoneOfAdventure.Core;

public class Zombie : MonoBehaviour
{
    Fighter fighter;
    private Animator anim;
    private Mover mover;
    private Flip flip;
    private ActionScheduler scheduler;
    EnemyDetector enemyDetector;
    private object target;
    private GameObject player;
    private float attackRange = 1f;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
        enemyDetector = GetComponentInChildren<EnemyDetector>();
        anim = GetComponent<Animator>();
        mover = GetComponent<Mover>();
        flip = GetComponent<Flip>();
        scheduler = GetComponent<ActionScheduler>();

        enemyDetector.PlayerDetected.AddListener(() => UpdateTarget());
        enemyDetector.PlayerLost.AddListener(() => UpdateTarget());      // TODO add idle state
    }

    private void Update()
    {
        if (player)
        {
            Debug.Log("have target");
            if (PlayerInAttackRange())
            {
                if (PlayerInFront())
                {
                    fighter.StartAttack();
                }
                else flip.FlipObject();
            }
            else
            {
                mover.MoveTo(CalculateDirection());
            }
        }
        else
        {
            scheduler.CancelCurrentAction();
        }
    }

    private bool PlayerInFront()
    {
        if (flip.isFacingRight && CalculateDirection() == 1f ||
            !flip.isFacingRight && CalculateDirection() == -1f)
            return true;
        else
            return false;
    }

    private float CalculateDirection()
    {
        return Mathf.Sign(player.transform.position.x - transform.position.x);
    }

    private bool PlayerInAttackRange()
    {
        return Mathf.Abs(transform.position.x - player.transform.position.x) <= attackRange;
    }

    public void UpdateTarget()
    {
        player = enemyDetector.Player;
    }
}
