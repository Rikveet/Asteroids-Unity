using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int maxScore;

    public PlayerData(world player)
    {
        maxScore = player.score;
    }
}
