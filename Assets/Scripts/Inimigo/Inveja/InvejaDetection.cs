using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvejaDetection : MonoBehaviour
{
    public string _tagTargetDetection = "Player";

    public List<Collider2D> detectsObj = new List<Collider2D>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _tagTargetDetection)
        {
            detectsObj.Add(collision);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _tagTargetDetection)
        {
            detectsObj.Remove(collision);
        }
    }

}
