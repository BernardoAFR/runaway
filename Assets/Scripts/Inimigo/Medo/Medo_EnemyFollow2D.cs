using System;
using UnityEngine;

public class Medo_EnemyFollow2D : MonoBehaviour
{
    public float speed = 7f;
    private Rigidbody2D rb;
    private string DirctHor = "";
    private string DirctVert = "";
    private Vector2 direction;
    public Transform player;

    // Raycast
    public float rayLength = 0.5f;
    public LayerMask collisionLayer;

    public Vector2 rightOffset = Vector2.zero;
    public Vector2 leftOffset = Vector2.zero;
    public Vector2 upOffset = Vector2.zero;
    public Vector2 downOffset = Vector2.zero;

    public bool isCollidingRight;
    public bool isCollidingLeft;
    public bool isCollidingUp;
    public bool isCollidingDown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Comparar();
        Check();
        Move();
    }

    void Move()
    {
        float moveX = 0f;
        float moveY = 0f;

        // Somente move se não houver colisão na direção
        if (DirctHor == "D" && !isCollidingRight)
            moveX = 1f;
        else if (DirctHor == "E" && !isCollidingLeft)
            moveX = -1f;

        if (DirctVert == "C" && !isCollidingUp)
            moveY = 1f;
        else if (DirctVert == "B" && !isCollidingDown)
            moveY = -1f;

        direction = new Vector2(moveX, moveY);

        // Aplica movimento somente se houver direção válida
        if (direction.sqrMagnitude > 0.01f)
        {
            direction = direction.normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    void Comparar()
    {
        if (player != null)
        {
            Vector2 myPos = rb.position;
            Vector2 delta = (Vector2)player.position - myPos;

            // Zona morta para evitar tremores
            if (delta.magnitude < 0.1f)
            {
                DirctHor = "";
                DirctVert = "";
                return;
            }

            DefinirDirecao(player.position);
        }
    }

    void DefinirDirecao(Vector2 destino)
    {
        Vector2 myPos = rb.position;

        // Define direção horizontal
        if (Mathf.Abs(destino.x - myPos.x) > 0.05f)
            DirctHor = destino.x > myPos.x ? "D" : "E";
        else
            DirctHor = "";

        // Define direção vertical
        if (Mathf.Abs(destino.y - myPos.y) > 0.05f)
            DirctVert = destino.y > myPos.y ? "C" : "B";
        else
            DirctVert = "";
    }

    void Check()
    {
        Vector2 position = rb.position;

        isCollidingRight = Physics2D.Raycast(position + rightOffset, Vector2.right, rayLength, collisionLayer);
        isCollidingLeft = Physics2D.Raycast(position + leftOffset, Vector2.left, rayLength, collisionLayer);
        isCollidingUp = Physics2D.Raycast(position + upOffset, Vector2.up, rayLength, collisionLayer);
        isCollidingDown = Physics2D.Raycast(position + downOffset, Vector2.down, rayLength, collisionLayer);

        // Debug dos Raycasts
        Debug.DrawRay(position + rightOffset, Vector2.right * rayLength, Color.red);
        Debug.DrawRay(position + leftOffset, Vector2.left * rayLength, Color.red);
        Debug.DrawRay(position + upOffset, Vector2.up * rayLength, Color.red);
        Debug.DrawRay(position + downOffset, Vector2.down * rayLength, Color.red);
    }
}
