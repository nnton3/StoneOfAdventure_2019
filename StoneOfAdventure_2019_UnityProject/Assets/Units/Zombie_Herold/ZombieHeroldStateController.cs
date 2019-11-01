using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using UnityEngine.Tilemaps;
using System.Collections;

public class ZombieHeroldStateController : Unit
{
    private Flip flip;
    private EnemyDetector enemyDetector;
    private object target;
    private GameObject player;
    private ZombieBuffer alliesDetector;
    private ZombieIdleState idleState;
    private Tilemap groundMap;


    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float movespeed = 3f;

    [SerializeField] private string currentState = "";
    [SerializeField] private float patrolDelay = 2f;
    [SerializeField] private TileBase groundTile;

    private void Start()
    {
        alliesDetector = GetComponentInChildren<ZombieBuffer>();
        flip = GetComponent<Flip>();
        groundMap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();

        idleState = GetComponent<ZombieIdleState>();

        DisableState();
        StartCoroutine("PatrolTimer");
    }

    private void Update()
    {
        PatrolBehaviour();

        MoveHorizontal(patrolDirection, movespeed);
        currentState = State.ToString();
    }

   [SerializeField] private float patrolDirection;
    private void PatrolBehaviour()
    {
        if (currentPatrolState == PatrolState.wait) patrolDirection = 0f;
        else patrolDirection = CalculateDirection();
    }

    private enum PatrolState { move, wait }
    [SerializeField] private PatrolState currentPatrolState = PatrolState.wait;
    IEnumerator PatrolTimer()
    {
        yield return new WaitForSeconds(patrolDelay);
        if (currentPatrolState == PatrolState.wait) currentPatrolState = PatrolState.move;
        else currentPatrolState = PatrolState.wait;
        StartCoroutine("PatrolTimer");
    }

    private float CalculateDirection()
    {
        Vector3 currentDirection = (flip.isFacingRight) ? Vector3.right : Vector3.left;
        TileBase nextTile = groundMap.GetTile(groundMap.WorldToCell(transform.position + Vector3.down + currentDirection));
        if (nextTile == groundTile)
        {
            return currentDirection.x;
        }
        else return -currentDirection.x;
    }

    private void MoveHorizontal(float direction, float movespeed) { State.MoveHorizontal(direction, movespeed); }

    public override void DisableState() { State = idleState; }

    public override void Dead()
    {
        State.Dead();
        alliesDetector.enabled = false;
    }

    public override void ApplyStun(float timeOfStun)
    {
        State.Stun(timeOfStun);
    }
}
