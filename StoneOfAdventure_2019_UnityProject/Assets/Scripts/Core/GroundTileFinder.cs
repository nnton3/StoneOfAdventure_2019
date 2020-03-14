using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using Zenject;

public class GroundTileFinder : MonoBehaviour
{
    [Inject] private Tilemap groundTilemap;
    [Inject] private TileBase targetTile;

    public List<Vector3> FindValidPositions(Vector3Int area, Vector3 center)
    {
        var groundTIlesPosition = new List<Vector3>();

        if (groundTilemap == null)
        {
            Debug.Log("Ground tilemap does not itialize");
            return groundTIlesPosition;
        }

        var startCheckPoint = center - (Vector3)area / 2;

        for (int i = 1; i < area.x; i++)
        {
            for (int j = 1; j < area.y; j++)
            {
                Vector3 worldPositionCheck = startCheckPoint + new Vector3(i, j, 0f);
                Vector3Int tilePositionCheck = groundTilemap.WorldToCell(worldPositionCheck);
                if (groundTilemap.GetTile(tilePositionCheck) == targetTile)
                {
                    groundTIlesPosition.Add(tilePositionCheck + Vector3Int.right);
                }
            }
        }

        return groundTIlesPosition;
    }

    public Vector3 PositionIsValid(Vector3 position)
    {
        var tilePositionCheck = groundTilemap.WorldToCell(position + Vector3.down);
        if (groundTilemap.GetTile(tilePositionCheck) == targetTile)
        {
            return tilePositionCheck + Vector3.up;
        }
        return Vector3.zero;
    }
}
