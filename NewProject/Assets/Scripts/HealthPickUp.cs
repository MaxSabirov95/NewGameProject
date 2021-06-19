using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] PointsManager _pointsManager;

    public int pointsToGive;
    public bool isPicked;

    private void Start()
    {
        isPicked = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("A");
            isPicked = true;
            //collision.gameObject.GetComponentInChildren<HealthScript>().CheckIfPickedBucket();
            gameObject.SetActive(false);
            _pointsManager.pointsCounter += pointsToGive;
        }
    }
}
