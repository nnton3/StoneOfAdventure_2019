using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using System;

public class GroundTileFinder : MonoBehaviour
{
    private Tilemap groundTilemap;
    private GameObject player;

    private TileBase targetTile;
    [SerializeField] private Vector3Int boundSize = new Vector3Int(1, 1, 1);

    private void Start()
    {
        player = FindObjectOfType<PlayerStateController>().gameObject;
        targetTile = Resources.Load<TileBase>("2");
        FindGroundTiles(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        SceneManager.sceneLoaded += FindGroundTiles;
    }

    private void FindGroundTiles(Scene arg0, LoadSceneMode arg1)
    {
        groundTilemap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();
    }

    public List<Vector3> FindValidPositions()
    {
        if (groundTilemap == null)
        {
            groundTilemap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();
            if (groundTilemap == null)
            {
                return new List<Vector3>();
            }
        }
        List<Vector3> groundTIlesPosition = new List<Vector3>();

        Vector3 startCheckPoint = player.transform.position - (Vector3)boundSize / 2;

        for (int i = 1; i < boundSize.x; i++)
        {
            for (int j = 1; j < boundSize.y; j++)
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
        Vector3Int tilePositionCheck = groundTilemap.WorldToCell(position + Vector3.down);
        if (groundTilemap.GetTile(tilePositionCheck) == targetTile)
        {
            return tilePositionCheck + Vector3.up;
        }
        return Vector3.zero;
    }
}
