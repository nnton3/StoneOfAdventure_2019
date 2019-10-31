using UnityEngine;
using StoneOfAdventure.Combat;
using StoneOfAdventure.Core;

public class ZombieHeroldStateController : Unit
{
    private Flip flip;
    private EnemyDetector enemyDetector;
    private object target;
    private GameObject player;
    private ZombieBuffer alliesDetector;
    private ZombieIdleState idleState;

    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float movespeed = 3f;

    [SerializeField] private string currentState = "";

    private void Start()
    {
        alliesDetector = GetComponentInChildren<ZombieBuffer>();
        flip = GetComponent<Flip>();

        idleState = GetComponent<ZombieIdleState>();

        DisableState();
    }

    private void Update()
    {
        MoveHorizontal(CalculateDirection(), movespeed);

        currentState = State.ToString();
    }

    private float CalculateDirection()
    {
        if (player)
        {
            return Mathf.Sign(player.transform.position.x - transform.position.x);
        }
        else return 0f;
    }

    public void UpdateTarget() { player = enemyDetector.Player; }

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
