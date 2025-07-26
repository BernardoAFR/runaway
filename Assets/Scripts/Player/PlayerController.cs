using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd;
    private Animator anim;

    public float speed;
    public Joystick joystick; // arraste o Joystick da cena aqui no inspector

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);

        if (direction.magnitude > 0.1f)
        {
            rd.velocity = direction.normalized * speed;
            anim.SetBool("IsRun", true);

            if (direction.x > 0) transform.eulerAngles = new Vector3(0f, 0f, 0f);
            else if (direction.x < 0) transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else
        {
            rd.velocity = Vector2.zero;
            anim.SetBool("IsRun", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inveja"))
        {
            Debug.Log("Colidiu com a Inveja");
            // Reiniciar a cena atual (se estiver usando UnityEngine.SceneManagement)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (collision.gameObject.CompareTag("Raiva"))
        {
            Debug.Log("Colidiu com a Raiva");
            // Reiniciar a cena atual (se estiver usando UnityEngine.SceneManagement)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (collision.gameObject.CompareTag("Medo"))
        {
            Debug.Log("Colidiu com o Medo");
            // Reiniciar a cena atual (se estiver usando UnityEngine.SceneManagement)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (collision.gameObject.CompareTag("Tristeza"))
        {
            Debug.Log("Colidiu com a Tristeza");
            // Reiniciar a cena atual (se estiver usando UnityEngine.SceneManagement)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
