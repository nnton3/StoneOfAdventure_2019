using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

public class GroundTileScanner : MonoBehaviour
{
    [Inject] private TileBase patrolTile;
    private Tilemap patrolMap;
    private Flip flip;

    private void Awake()
    {
        patrolMap = GameObject.FindGameObjectWithTag("PatrolTile").GetComponent<Tilemap>();
        flip = GetComponent<Flip>();
    }

    public bool UnitOnTheGround()
    {
        Vector3 currentDirection = (flip.isFacingRight) ? Vector3.right : Vector3.left;
        TileBase nextTile = patrolMap.GetTile(patrolMap.WorldToCell(transform.position + Vector3.down + currentDirection / 2));
        return nextTile == patrolTile;
    }
}
