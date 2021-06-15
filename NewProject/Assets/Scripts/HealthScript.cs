using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] PointsManager _pointsManager;
    [SerializeField] HealthPickUp _healthPickUp;

    private SpriteRenderer player;
    private float duration = 15f;
    private float t = 0;

    private void Start()
    {
        player = GetComponentInChildren<SpriteRenderer>();
        _pointsManager = FindObjectOfType<PointsManager>();
        _healthPickUp = FindObjectOfType<HealthPickUp>();
    }

    private void FixedUpdate()
    {
        CheckIfPickedBucket();
    }

    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(1f);

        player.color = Color.Lerp(Color.white, Color.black, t);

        if(t<1)
        {
            t += Time.fixedDeltaTime / duration;
        }

        if(player.color == Color.black)
        {
            _pointsManager.SaveHighestPoints();
        }
    }

    IEnumerator AddColor()
    {
        yield return null;
        player.color = Color.white;
        t = 0;
        yield return new WaitForSeconds(2f);
        _healthPickUp.isPicked = false;
    }


    public void CheckIfPickedBucket()
    {
        if (!_healthPickUp.isPicked)
        {
            StartCoroutine(ChangeColor());
        }

        else
        {
            StopCoroutine(ChangeColor());
            StartCoroutine(AddColor());
        }
    }
}
