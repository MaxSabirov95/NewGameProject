using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    public GameObject platformDestructionPoint;

    private void Start()
    {
        platformDestructionPoint = GameObject.Find("PlatformDestructionPoint");
    }

    private void Update()
    {
        if(transform.position.x < platformDestructionPoint.transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}
