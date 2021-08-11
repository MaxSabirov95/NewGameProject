using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinPOV_Increaser : MonoBehaviour
{
    [SerializeField]
    float newZ;
    [SerializeField]
    float time;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player p = collision.GetComponentInParent<Player>();

            p.CallCamToFOV(newZ, time);

            Destroy(gameObject);
        }
    }
    
}
