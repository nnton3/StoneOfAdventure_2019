using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using System.Collections;

public class ZombieStateController : Unit
{
    private Flip flip;
    private EnemyDetector enemyDetector;
    private object target;
    private GameObject player;
    private ZombieIdleState idleState;
    private BaseState deathState;
    private PatrolBehaviour patrolBehaviour;

    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float movespeed = 3f;

    [SerializeField] private string currentState = "";

    private void Start()
    {
        enemyDetector = GetComponentInChildren<EnemyDetector>();
        flip = GetComponent<Flip>();
        patrolBehaviour = GetComponent<PatrolBehaviour>();

        idleState = GetComponent<ZombieIdleState>();
        deathState = GetComponent<ZombieDeathState>();

        DisableState();
        
        enemyDetector.PlayerDetected.AddListener(() => UpdateTarget());
        enemyDetector.PlayerLost.AddListener(() => UpdateTarget());
    }

    private void Update()
    {
        Debug.Log(State.ToString());
        if (State == deathState) return;
        if (player)
        {
            if (PlayerInAttackRange())
            {
                if (PlayerInFront()) Attack();
            }
            MoveHorizontal(CalculateDirection(), movespeed);
        }
        else
        {
            patrolBehaviour.UpdatePatrolBehaviour();
            MoveHorizontal(patrolBehaviour.PatrolDirection, movespeed);
        }

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

    public void UpdateTarget()
    {
        player = enemyDetector.Player;
        if (player == null) DisableState();
    }

    private void Attack() { State.Attack(); }

    private void MoveHorizontal(float direction, float movespeed) { State.MoveHorizontal(direction, movespeed); }

    public override void DisableState() { State = idleState; }

    public override void Dead()
    {
        State.Dead();
        enemyDetector.PlayerDetected.RemoveAllListeners();
        enemyDetector.PlayerLost.RemoveAllListeners();
        StartCoroutine("DestroyCorrupse");
    }

    IEnumerator DestroyCorrupse()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public override void ApplyStun(float timeOfStun)
    {
        State.Stun(timeOfStun);
    }
}
