using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgraderManager : MonoBehaviour
{
    public static UpgraderManager instance;
    public ButtonUpgrade[] upgradesButtons;
    public int totalPoint = 0;
    [SerializeField] private Text _totalPointsText;

    private void Awake()
    {
        instance = this;
    }
    private void OnDestroy()
    {
        instance = null;
    } 

    private void Start()
    {
        try
        {
            LoadProgression();
        }
        catch (System.Exception)
        {

            Debug.Log("First Time Total Point");
        }        
        UpdateTotalPoint();
        CheckPoints();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            totalPoint += 250;
            UpdateTotalPoint();
            CheckPoints();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void CheckPoints()
    {
        foreach (ButtonUpgrade button in upgradesButtons)
        {
            if (button.upgradeLevel < 6)
            {
                if (button.priseToBuy > totalPoint)
                {
                    button.thisButton.interactable = false;
                }
                else
                {
                    button.thisButton.interactable = true;
                }
            }
            else
            {
                button.thisButton.interactable = false;
            }
        }
    }

    public void UpdateTotalPoint()
    {
        _totalPointsText.text = "Total Points: " + totalPoint.ToString();
    }

    public void SaveProgression()
    {
        PlayerPrefs.SetInt("TotalPoints", totalPoint);
        //SaveLoadManager.instance.Save("TotalPoints",0, totalPoint);
    }

    public void LoadProgression()
    {
        totalPoint = PlayerPrefs.GetInt("TotalPoints");
        //SaveLoadManager.instance.Load(totalPoint, "TotalPoints", 0);
    }
}
