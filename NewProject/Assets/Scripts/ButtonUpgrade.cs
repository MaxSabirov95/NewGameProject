using UnityEngine;
using UnityEngine.UI;

public class ButtonUpgrade : MonoBehaviour
{
    public enum Upgrades { Colorful, Acuurancy, Coloring }
    public Upgrades upgrades;
    public int priseToBuy;
    public int upgradeLevel;
    public Text prise;
    public Text level;
    public Button thisButton;
    public int grovingPrise = 25;

    private void Start()
    {
        try
        {
            LoadUpgrade();
        }
        catch (System.Exception)
        {

            Debug.Log("FirstTime " + (int)upgrades);
        }
        if (upgradeLevel <= 0)
        {
            upgradeLevel = 1;
        }
        priseToBuy += (grovingPrise * upgradeLevel);
        UpdateLevel();
        UpdatePrise();
    }

    public void Upgrade()
    {
        if (upgradeLevel < 6)
        {
            if (priseToBuy <= UpgraderManager.instance.totalPoint)
            {
                UpgraderManager.instance.totalPoint -= priseToBuy;
                upgradeLevel++;
                UpdateLevel();

                switch (upgrades)
                {
                    case Upgrades.Colorful:
                        GameData.ColorfulLevel = upgradeLevel;
                        Debug.Log(upgrades + " level: " + GameData.ColorfulLevel);

                        /*float _duration = PlayerPrefs.GetFloat("Duration");
                        _duration += 2f;
                        PlayerPrefs.SetFloat("Duration", _duration);*/
                        break;
                    case Upgrades.Acuurancy:
                        GameData.AccuracyLevel = upgradeLevel;
                        Debug.Log(upgrades + " level: " + GameData.AccuracyLevel);
                        break;
                    case Upgrades.Coloring:
                        GameData.ColoringLevel = upgradeLevel;
                        Debug.Log(upgrades + " level: " + GameData.ColoringLevel);
                        break;
                }

                UpgraderManager.instance.CheckPoints();
                SaveUpgrade();
                UpgraderManager.instance.SaveProgression();
                UpgraderManager.instance.UpdateTotalPoint();
                UpdateLevel();
                priseToBuy += grovingPrise;
                UpdatePrise();
            }
        }
    }

    void UpdatePrise()
    {
        prise.text = "Cost: " + priseToBuy.ToString();
    }
    void UpdateLevel()
    {
        if (upgradeLevel < 6)
        {
            level.text = "Level: " + upgradeLevel.ToString();
        }
        else
        {
            level.text = "Complete Upgraded";
        }
    }

    void SaveUpgrade()
    {
        PlayerPrefs.SetInt("Upgrade" + (int)upgrades, upgradeLevel);
        //SaveLoadManager.instance.Save("Upgrade",(int)upgrades, upgradeLevel);
    }

    void LoadUpgrade()
    {
        upgradeLevel = PlayerPrefs.GetInt("Upgrade" + (int)upgrades);
        //SaveLoadManager.instance.Load(upgradeLevel, "Upgrade", (int)upgrades);
    }
}
