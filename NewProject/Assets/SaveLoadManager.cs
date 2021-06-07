using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager instance;

    void Start()
    {
        instance = this;
    }
    
    public void Save(string name,int num, int number)
    {
        PlayerPrefs.SetInt(name+num, number);
    }

    public void Load(int property, string name, int num)
    {
        property = PlayerPrefs.GetInt(name+num);
    }

    public void DeleteSaveFile()
    {
        PlayerPrefs.DeleteAll();
    }// --For Tests--

    private void OnDestroy()
    {
        instance = null;
    }
}
