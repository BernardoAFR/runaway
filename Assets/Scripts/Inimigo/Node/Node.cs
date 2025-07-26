using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Vector3Int gridPosition;
    public List<Node> neighbors = new List<Node>();

    public void Initialize(Vector3Int pos)
    {
        gridPosition = pos;
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.2f);

        Gizmos.color = Color.cyan;
        foreach (Node neighbor in neighbors)
        {
            if (neighbor != null)
            {
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }
    }
    
}
