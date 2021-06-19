using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject thePlatform;
    public Transform generationPoint;
    public float distanceBetweenMin;
    public float distanceBetweenMax;

    private float distanceBetween;
    private float platformWidth;

    private GameObject go;

    private void Start()
    {
        platformWidth = thePlatform.GetComponent<Transform>().localScale.x;
        
    }

    private void Update()
    {
        if(transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween, transform.position.y, transform.position.z);

            go = Instantiate(thePlatform, transform.position, transform.rotation);
        }
    }
}
