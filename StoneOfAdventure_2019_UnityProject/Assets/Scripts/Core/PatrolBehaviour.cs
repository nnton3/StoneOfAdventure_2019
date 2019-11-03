using System.Collections;
using UnityEngine.Tilemaps;
using UnityEngine;

public class PatrolBehaviour : MonoBehaviour
{
    private Tilemap groundMap;
    [SerializeField] private float patrolDelay = 2f;
    [SerializeField] private TileBase groundTile;
    private Flip flip;

    private void Start()
    {
        flip = GetComponent<Flip>();
        groundMap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();

        StartCoroutine("PatrolTimer");
    }

    public float PatrolDirection { get; private set; }
    public void UpdatePatrolBehaviour()
    {
        if (currentPatrolState == PatrolState.wait) PatrolDirection = 0f;
        else PatrolDirection = CalculateDirection();
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

    public void Cancel()
    {
        StopCoroutine("PatrolTimer");
        PatrolDirection = 0f;
    }
}
