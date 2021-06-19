using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsManager : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI highestPointsText;
    public float pointsCounter;
    public float highestPointsCounter;
    public float pointsPerSecond;
    public bool pointsIncreasing;

    private void Start()
    {
        DeleteHighestPoints();
        DeletePoints();
        pointsCounter = 0;

        if (PlayerPrefs.HasKey("Highest Points"))
        {
            LoadHighestPoints();
        }
    }

    private void Update()
    {
        if (pointsIncreasing)
        {
            pointsCounter += pointsPerSecond * Time.deltaTime;

            if (pointsCounter > highestPointsCounter)
            {
                highestPointsCounter = pointsCounter;
            }

            pointsText.text = "Points: " + Mathf.Round(pointsCounter);
            highestPointsText.text = "Highest Points: " + Mathf.Round(highestPointsCounter);
        }


        //Need to add: stop increasing points when dying.
    }

    public void SaveHighestPoints()
    {
        PlayerPrefs.SetFloat("Highest Points", highestPointsCounter);
    }

    public void LoadHighestPoints()
    {
        highestPointsCounter = PlayerPrefs.GetFloat("Highest Points");
    }

    public void DeleteHighestPoints()
    {
        PlayerPrefs.DeleteKey("Highest Points");
    }

    public void SavePoints()
    {
        PlayerPrefs.SetFloat("Points", pointsCounter);
    }

    public void LoadPoints()
    {
        pointsCounter = PlayerPrefs.GetFloat("Points");
    }

    public void DeletePoints()
    {
        PlayerPrefs.DeleteKey("Points");
    }

}
