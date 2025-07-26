using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class NodeGenerator : MonoBehaviour
{
    public Tilemap groundTilemap;
    public GameObject nodePrefab;

    private Dictionary<Vector3Int, Node> nodeDictionary = new Dictionary<Vector3Int, Node>();

    void Awake()
    {
        GenerateNodes();
        AssignNeighbors();
    }

    void GenerateNodes()
    {
        BoundsInt bounds = groundTilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                if (groundTilemap.HasTile(tilePos))
                {
                    Vector3 worldPos = groundTilemap.CellToWorld(tilePos) + groundTilemap.cellSize / 2;
                    GameObject nodeObj = Instantiate(nodePrefab, worldPos, Quaternion.identity);
                    Node node = nodeObj.GetComponent<Node>();
                    node.Initialize(tilePos);
                    nodeDictionary.Add(tilePos, node);
                }
            }
        }
    }

    void AssignNeighbors()
    {
        Vector3Int[] directions = new Vector3Int[]
        {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right
        };

        foreach (var kvp in nodeDictionary)
        {
            Node node = kvp.Value;

            foreach (Vector3Int dir in directions)
            {
                Vector3Int neighborPos = node.gridPosition + dir;
                if (nodeDictionary.ContainsKey(neighborPos))
                {
                    node.neighbors.Add(nodeDictionary[neighborPos]);
                }
            }
        }
    }
}
