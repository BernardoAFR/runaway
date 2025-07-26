using System;
using Unity.VisualScripting;
using UnityEngine;

public class Raiva_EnemyFollow2D : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody2D rb;
    private Vector2 direction;
    private int movDirc;
    
    // Variaveis para o Raycast
    public float rayLength = 1f;
    public LayerMask collisionLayer;

    public Vector2 rightOffset = new Vector2(0, 0);  // desloca o ponto do ray para a direita
    public Vector2 leftOffset = new Vector2(0, 0);   // para a esquerda
    public Vector2 upOffset = new Vector2(0, 0);      // para cima
    public Vector2 downOffset = new Vector2(0, 0);   // para baixo

    public bool isCollidingRight;
    public bool isCollidingLeft;
    public bool isCollidingUp;
    public bool isCollidingDown;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        movDirc = UnityEngine.Random.Range(1, 9); // 1-direita, 2-esquerda, 3-cima, 4-baixo, 5+ Diagonais(1 até 8)
    }
    
    void FixedUpdate()
    {
        Check();
        Move();
        HandleCollision();
    }

    void Move()
    {
        switch (movDirc)
        {
            case 1: direction = Vector2.right; break;
            case 2: direction = Vector2.left; break;
            case 3: direction = Vector2.up; break;
            case 4: direction = Vector2.down; break;
            case 5: direction = (Vector2.right + Vector2.up).normalized; break;    // Direita-Cima
            case 6: direction = (Vector2.right + Vector2.down).normalized; break;  // Direita-Baixo
            case 7: direction = (Vector2.left + Vector2.up).normalized; break;     // Esquerda-Cima
            case 8: direction = (Vector2.left + Vector2.down).normalized; break;   // Esquerda-Baixo
        }
        
        rb.velocity = direction * speed;
    }

    void Check(){
        Vector2 position = transform.position;

        // Raycast para a Direita
        isCollidingRight = Physics2D.Raycast(position + rightOffset, Vector2.right, rayLength, collisionLayer);

        // Raycast para a Esquerda
        isCollidingLeft = Physics2D.Raycast(position + leftOffset, Vector2.left, rayLength, collisionLayer);

        // Raycast para Cima
        isCollidingUp = Physics2D.Raycast(position + upOffset, Vector2.up, rayLength, collisionLayer);

        // Raycast para Baixo
        isCollidingDown = Physics2D.Raycast(position + downOffset, Vector2.down, rayLength, collisionLayer);

        // Visualizar os raycasts no editor
        Debug.DrawRay(position + rightOffset, Vector2.right * rayLength, Color.red);
        Debug.DrawRay(position + leftOffset, Vector2.left * rayLength, Color.red);
        Debug.DrawRay(position + upOffset, Vector2.up * rayLength, Color.red);
        Debug.DrawRay(position + downOffset, Vector2.down * rayLength, Color.red);
    }

    void HandleCollision(){
        bool shouldChangeDirection = false;

        switch (movDirc){
            case 1: if (isCollidingRight) shouldChangeDirection = true; break;
            case 2: if (isCollidingLeft) shouldChangeDirection = true; break;
            case 3: if (isCollidingUp) shouldChangeDirection = true; break;
            case 4: if (isCollidingDown) shouldChangeDirection = true; break;
            case 5: if (isCollidingRight || isCollidingUp) shouldChangeDirection = true; break;
            case 6: if (isCollidingRight || isCollidingDown) shouldChangeDirection = true; break;
            case 7: if (isCollidingLeft || isCollidingUp) shouldChangeDirection = true; break;
            case 8: if (isCollidingLeft || isCollidingDown) shouldChangeDirection = true; break;
        }

        if (shouldChangeDirection)
        {
            ChooseNewDirection();
        }
    }

    void ChooseNewDirection(){
        int newDir = movDirc;
        // Garante que a nova direção seja diferente da atual
        while (newDir == movDirc)
        {
            newDir = UnityEngine.Random.Range(1, 9); // 1 a 8
        }
        movDirc = newDir;
    }
}
