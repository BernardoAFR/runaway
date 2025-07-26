using System.Collections.Generic;
using UnityEngine;

public class Tristeza_EnemyFollow2D : MonoBehaviour
{
    public AStarPathfinder pathfinder;
    public float speed = 9f;

    private List<Node> path = new List<Node>();
    private int currentPathIndex = 0;

    private void Awake()
    {
        if (pathfinder == null)
        {
            pathfinder = FindObjectOfType<AStarPathfinder>();
        }

        InvokeRepeating(nameof(UpdatePath), 0f, 0.7f);                      // Atualiza o caminho a cada X segundo
    }

    void UpdatePath()
    {
        path = pathfinder.FindPath();
        currentPathIndex = 0;
    }
    
    void FixedUpdate()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (path == null || path.Count == 0 || currentPathIndex >= path.Count)
            return;

        Node targetNode = path[currentPathIndex];
        Vector2 targetPosition = targetNode.transform.position;

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;

        if (Vector2.Distance(transform.position, targetPosition) < 0.6f)    // inimigo "chegou" a um nó e assim avança para o próximo
        {
            currentPathIndex++;
        }
    }
}
