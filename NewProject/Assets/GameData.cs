using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static int ColorfulLevel;
    public static int AccuracyLevel;
    public static int ColoringLevel;

    public void SetPlayerColorfulLevel(int playerColorful)
    {
        playerColorful += (ColorfulLevel - 1) * 10;
    }

    public void SetPlayerAccuracyLevel(int playerAccuracy)
    {
        playerAccuracy += (AccuracyLevel - 1) * 10;
    }

    public void SetPlayerColoringLevel(int playerColoring)
    {
        playerColoring += (ColoringLevel - 1) * 10;
    }
}
