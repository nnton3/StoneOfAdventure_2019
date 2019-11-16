using System;
using System.Collections;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using UnityEngine;

public class KnightAttacker_StateController : Unit
{
    //#region Variables
    //private Flip flip;
    //private EnemyDetector enemyDetector;
    //private object target;
    //private GameObject player;
    //public IKnightAttackerState State;
    //private KnightAttacker_IdleState idleState;

    //[SerializeField] private float attackRange = 1f;
    //[SerializeField] private float movespeed = 3f;

    //[SerializeField] private string currentState = "";
    //[SerializeField] private float spurtDelay = 5f;
    //#endregion
    //private void Start()
    //{
    //    enemyDetector = GetComponentInChildren<EnemyDetector>();
    //    flip = GetComponent<Flip>();

    //    idleState = GetComponent<KnightAttacker_IdleState>();

    //    DisableState();

    //    enemyDetector.PlayerDetected.AddListener(() =>
    //    {
    //        UpdateTarget();
    //        StartSpurtTimer();
    //    });
    //    enemyDetector.PlayerLost.AddListener(() =>
    //    {
    //        UpdateTarget();
    //        StopSpurtTimer();
    //    });
    //}

    //private void Update()
    //{
    //    if (player)
    //    {
    //        if (PlayerInAttackRange())
    //        {
    //            if (PlayerInFront()) Attack();
    //        }
    //    }
    //    else
    //        DisableState();

    //    MoveHorizontal(CalculateDirection(), movespeed);

    //    currentState = State.ToString();
    //}

    //private void StopSpurtTimer()
    //{
    //    Debug.Log("stop");
    //    StopCoroutine("SpurtTimer");
    //}

    //private void StartSpurtTimer()
    //{
    //    Debug.Log("start");
    //    StartCoroutine("SpurtTimer");
    //}

    //private IEnumerator SpurtTimer()
    //{
    //    yield return new WaitForSeconds(spurtDelay);
    //    Debug.Log("teleport");
    //    Spurt();
    //    StartSpurtTimer();
    //}

    //private bool PlayerInFront()
    //{
    //    if (flip.isFacingRight && CalculateDirection() == 1f ||
    //        !flip.isFacingRight && CalculateDirection() == -1f)
    //        return true;
    //    else return false;
    //}

    //private float CalculateDirection()
    //{
    //    if (player)
    //    {
    //        return Mathf.Sign(player.transform.position.x - transform.position.x);
    //    }
    //    else return 0f;
    //}

    //private bool PlayerInAttackRange()
    //{
    //    return Mathf.Abs(transform.position.x - player.transform.position.x) <= attackRange;
    //}

    //public void UpdateTarget() { player = enemyDetector.Player; }

    //private void Attack() { State.Attack(); }

    //private void Spurt() { State.Spurt(); }

    //private void MoveHorizontal(float direction, float movespeed) { State.MoveHorizontal(direction, movespeed); }

    //public override void DisableState() { State = idleState; }

    //public override void Dead()
    //{
    //    State.Dead();
    //    enemyDetector.enabled = false;
    //}

    //public override void ApplyStun(float timeOfStun)
    //{
    //    State.Stun(timeOfStun);
    //}
}
