using UnityEngine;

public class Inveja_EnemyFollow2D : MonoBehaviour
{
    public float speed = 8f;
    private Vector2 direction;
    private Rigidbody2D rb;
    public InvejaDetection detectionArea;

    private Vector2 randomDirection;
    private float changeDirectionTime = 1.5f; // tempo para mudar direção aleatória
    private float changeDirectionTimer;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (detectionArea.detectsObj.Count > 0)
        {
            direction = (detectionArea.detectsObj[0].transform.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
        else
        {
            changeDirectionTimer -= Time.fixedDeltaTime;
            if (changeDirectionTimer <= 0)
            {
                SetRandomDirection();
            }
            rb.MovePosition(rb.position + randomDirection * speed * Time.fixedDeltaTime);
        }
    }

    void SetRandomDirection()
    {
        // Direção aleatória: X e Y entre -1 e 1
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        changeDirectionTimer = changeDirectionTime;
    }
}
