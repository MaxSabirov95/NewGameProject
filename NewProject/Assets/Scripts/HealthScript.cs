using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] PointsManager _pointsManager;
    [SerializeField] SceneLoader _sceneLoader;
    [SerializeField] HealthPickUp _healthPickUp;

    private SpriteRenderer player;
    [SerializeField] private float duration = 15f;
    private float t = 0;

    private void Start()
    {
        player = GameObject.Find("GFX_Hair").GetComponent<SpriteRenderer>();
        _pointsManager = FindObjectOfType<PointsManager>();
        _healthPickUp = FindObjectOfType<HealthPickUp>();
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void FixedUpdate()
    {
        CheckIfPickedBucket();
        PlayerPrefs.SetFloat("Duration", duration);
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
            _pointsManager.SavePoints();
            _pointsManager.pointsIncreasing = false;
            _sceneLoader.LoadScene("Losing Screen");
        }
    }

    IEnumerator AddColor()
    {
        yield return null;
        player.color = Color.white;
        t = 0;
        yield return new WaitForSeconds(1f);
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
