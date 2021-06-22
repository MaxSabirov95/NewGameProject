using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject _buttonsPanel;
    [SerializeField] GameObject _upgradePanel;
    private bool _isUpgradePanelOn = false;

    public void UpgradePanel()
    {
        _isUpgradePanelOn = !_isUpgradePanelOn;
        _buttonsPanel.SetActive(!_isUpgradePanelOn);
        _upgradePanel.SetActive(_isUpgradePanelOn);
    }
}
