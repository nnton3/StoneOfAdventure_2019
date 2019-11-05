using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundTileScanner : MonoBehaviour
{
    [SerializeField] private TileBase groundTile;
    private Tilemap groundMap;
    private Flip flip;

    private void Start()
    {
        groundMap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();
        flip = GetComponent<Flip>();
    }

    public bool UnitOnTheGround()
    {
        Vector3 currentDirection = (flip.isFacingRight) ? Vector3.right : Vector3.left;
        TileBase nextTile = groundMap.GetTile(groundMap.WorldToCell(transform.position + Vector3.down + currentDirection));
        return nextTile == groundTile;
    }
}
