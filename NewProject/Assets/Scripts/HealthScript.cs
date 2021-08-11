using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public List<HealthPickUp> flowers = new List<HealthPickUp>();

    [SerializeField] PointsManager _pointsManager;
    [SerializeField] SceneLoader _sceneLoader;
    //[SerializeField] HealthPickUp _healthPickUp;

    private SpriteRenderer player;
    [SerializeField] private float duration = 15f;
    //[SerializeField] private float _time = 10f;
    private float t = 0;

    //private float currentHP = 0f;
    //private float maxHP = 100f;

    //private float healthPrecentage;
    //private float minusHealthPerDistance = 1f;

    private void Start()
    {
        player = GameObject.Find("GFX_Hair").GetComponent<SpriteRenderer>();
        _pointsManager = FindObjectOfType<PointsManager>();
        //_healthPickUp = FindObjectOfType<HealthPickUp>();
        _sceneLoader = FindObjectOfType<SceneLoader>();
        //currentHP = maxHP;
        //healthPrecentage = currentHP / maxHP;
    }

    private void FixedUpdate()
    {
        CheckIfPickedFlower();
        PlayerPrefs.SetFloat("Duration", duration);
    }

    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(1f);
        //healthPrecentage = currentHP / maxHP;
        //currentHP -= minusHealthPerDistance;
        //Debug.Log(currentHP);
        player.color = Color.Lerp(Color.white, Color.black, t);

        if (t < 1)
        {
            t += Time.fixedDeltaTime / duration;
        }

        //if (player.color == Color.black) //should compare t maybe? int is better
        //{
        //    //_pointsManager.SaveHighestPoints();
        //    _pointsManager.SavePoints();
        //    _pointsManager.pointsIncreasing = false;
        //    _sceneLoader.LoadScene("Death Screen");
        //}
    }

    IEnumerator AddColor()
    {
        foreach (HealthPickUp flower in flowers)
        {
            yield return null;
            player.color = Color.white;
            //_time = 10f;
            t = 0;
            yield return new WaitForSeconds(1f);
            flower.isPicked = false;
        }
    }


    public void CheckIfPickedFlower()
    {
        foreach (HealthPickUp flower in flowers)
        {
            if (!flower.isPicked)
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
}
