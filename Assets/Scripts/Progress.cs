using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Progress
{
    public float ScoreValue;

    public Progress()
    {
        ScoreValue = GameManager.instance.PlayerScore;
    }
}
