using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BW_Tile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (collision.CompareTag("Player"))
            {
                Collider2D myCol = GetComponentInParent<Collider2D>();
                myCol.isTrigger = false;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (collision.CompareTag("Player"))
            {
                Collider2D myCol = GetComponentInParent<Collider2D>();
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                myCol.isTrigger = false;

                sr.color = Color.red;
            }
        }
    }
}
