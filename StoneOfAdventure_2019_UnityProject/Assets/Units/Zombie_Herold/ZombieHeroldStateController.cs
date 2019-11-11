using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;
using System.Collections;
using UnityEngine.Experimental.VFX;

public class ZombieHeroldStateController : UnitContainsAward
{
    private Flip flip;
    private EnemyDetector enemyDetector;
    private object target;
    private GameObject player;
    private ZombieBuffer alliesDetector;
    private ZombieIdleState idleState;
    private PatrolBehaviour patrolBehaviour;

    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float movespeed = 3f;

    [SerializeField] private string currentState = "";

    protected override void Start()
    {
        alliesDetector = GetComponentInChildren<ZombieBuffer>();
        flip = GetComponent<Flip>();
        patrolBehaviour = GetComponent<PatrolBehaviour>();

        idleState = GetComponent<ZombieIdleState>();

        DisableState();
    }

    private void Update()
    {
        patrolBehaviour.UpdatePatrolBehaviour();

        MoveHorizontal(patrolBehaviour.PatrolDirection, movespeed);
        currentState = State.ToString();
    }

    private void MoveHorizontal(float direction, float movespeed) { State.MoveHorizontal(direction, movespeed); }

    public override void DisableState() { State = idleState; }

    public override void Dead()
    {
        State.Dead();
        alliesDetector.enabled = false;
        StartCoroutine("DestroyCorrupse");
        GetComponentInChildren<VisualEffect>().enabled = false;
    }

    private IEnumerator DestroyCorrupse()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public override void ApplyStun(float timeOfStun)
    {
        State.Stun(timeOfStun);
    }
}
