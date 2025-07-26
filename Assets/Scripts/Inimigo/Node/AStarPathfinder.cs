using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinder : MonoBehaviour
{
    public Transform player;
    public Transform enemy;

    private List<Node> openList = new List<Node>();
    private HashSet<Node> closedList = new HashSet<Node>();

    private Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
    private Dictionary<Node, float> gScore = new Dictionary<Node, float>();
    private Dictionary<Node, float> fScore = new Dictionary<Node, float>();

    private Node[] allNodes;

    private void Awake()
    {
        allNodes = FindObjectsOfType<Node>();
    }

    private Node GetClosestNode(Vector3 position)
    {
        Node closestNode = null;
        float minDistance = Mathf.Infinity;

        foreach (Node node in allNodes)
        {
            float dist = Vector3.Distance(position, node.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closestNode = node;
            }
        }

        return closestNode;
    }

    public List<Node> FindPath()
    {
        Node startNode = GetClosestNode(enemy.position);
        Node targetNode = GetClosestNode(player.position);

        if (startNode == null || targetNode == null)
        {
            //Debug.LogWarning("Start or Target Node not found!");
            return null;
        }

        openList.Clear();
        closedList.Clear();
        cameFrom.Clear();
        gScore.Clear();
        fScore.Clear();

        openList.Add(startNode);
        gScore[startNode] = 0;
        fScore[startNode] = Heuristic(startNode, targetNode);

        while (openList.Count > 0)
        {
            Node current = GetNodeWithLowestFScore(openList);

            if (current == targetNode)
            {
                return ReconstructPath(cameFrom, current);
            }

            openList.Remove(current);
            closedList.Add(current);

            foreach (Node neighbor in current.neighbors)
            {
                if (closedList.Contains(neighbor))
                    continue;

                float tentativeGScore = gScore[current] + Vector3Int.Distance(current.gridPosition, neighbor.gridPosition);

                if (!openList.Contains(neighbor))
                    openList.Add(neighbor);
                else if (tentativeGScore >= gScore.GetValueOrDefault(neighbor, Mathf.Infinity))
                    continue;

                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = tentativeGScore + Heuristic(neighbor, targetNode);
            }
        }

        return null; // No path found
    }

    private Node GetNodeWithLowestFScore(List<Node> nodes)
    {
        Node lowest = nodes[0];
        float lowestF = fScore.GetValueOrDefault(lowest, Mathf.Infinity);

        foreach (Node node in nodes)
        {
            float nodeF = fScore.GetValueOrDefault(node, Mathf.Infinity);
            if (nodeF < lowestF)
            {
                lowest = node;
                lowestF = nodeF;
            }
        }

        return lowest;
    }

    private List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
    {
        List<Node> totalPath = new List<Node> { current };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Insert(0, current);
        }

        return totalPath;
    }

    private float Heuristic(Node a, Node b)
    {
        return Mathf.Abs(a.gridPosition.x - b.gridPosition.x) + Mathf.Abs(a.gridPosition.y - b.gridPosition.y);
    }

    // Visualização opcional
    private void OnDrawGizmos()
    {
        if (player != null && enemy != null)
        {
            allNodes = FindObjectsOfType<Node>();
            List<Node> path = FindPath();

            if (path != null)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Gizmos.DrawLine(path[i].transform.position, path[i + 1].transform.position);
                }
            }
        }
    }
}
