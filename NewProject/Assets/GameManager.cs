using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject buttonsPanel;
    private bool _isInShop;

    private void Start()
    {
        _isInShop = false;
    }

    public void ShopInGame()
    {
        _isInShop = !_isInShop;
        shopPanel.SetActive(_isInShop);
        buttonsPanel.SetActive(!_isInShop);
    }
}
