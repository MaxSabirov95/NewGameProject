using UnityEngine;
using UnityEngine.UI;

public class ButtonUpgrade : MonoBehaviour
{
    public enum Upgrades { upgrade1, upgrade2, upgrade3, upgrade4 }
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
        if (upgradeLevel < 5)
        {
            if (priseToBuy <= UpgraderManager.instance.totalPoint)
            {
                UpgraderManager.instance.totalPoint -= priseToBuy;
                upgradeLevel++;
                UpdateLevel();
                //switch (upgrades)
                //{
                //    case Upgrades.upgrade1:
                //        break;
                //    case Upgrades.upgrade2:
                //        break;
                //    case Upgrades.upgrade3:
                //        break;
                //    case Upgrades.upgrade4:
                //        break;
                //}

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
        if (upgradeLevel < 5)
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
