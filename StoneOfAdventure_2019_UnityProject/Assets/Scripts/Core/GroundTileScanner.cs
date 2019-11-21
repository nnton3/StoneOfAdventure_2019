using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundTileScanner : MonoBehaviour
{
    private TileBase patrolTile;
    private Tilemap patrolMap;
    private Flip flip;

    private void Start()
    {
        patrolMap = GameObject.FindGameObjectWithTag("PatrolTile").GetComponent<Tilemap>();
        patrolTile = Resources.Load<TileBase>("2");
        flip = GetComponent<Flip>();
    }

    public bool UnitOnTheGround()
    {
        Vector3 currentDirection = (flip.isFacingRight) ? Vector3.right : Vector3.left;
        TileBase nextTile = patrolMap.GetTile(patrolMap.WorldToCell(transform.position + Vector3.down));// + currentDirection));
        return nextTile == patrolTile;
    }
}
