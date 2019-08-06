using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text PlayerScore;
    GameManager manager;

    void Awake()
    {
        manager = GameManager.instance;
        manager.ScoreUpCallBack += UpdateScoreUI;
    }

    public void UpdateScoreUI()
    {
        if(manager == null)
        {
            manager = GameManager.instance;
        }
        if (manager.PlayerScore <= 1000) PlayerScore.text = manager.PlayerScore.ToString();
        else if (manager.PlayerScore > 1000 && manager.PlayerScore < 1000000)
        {
            PlayerScore.text = (manager.PlayerScore / 1000).ToString() + "k";
        }
        else if (manager.PlayerScore >= 1000000)
        {
            PlayerScore.text = (manager.PlayerScore / 1000000).ToString() + "m";
        }
    }

    void UpdateScoreUI2()
    {
        bool temp = manager.PlayerScore >= 1000000 ? true : false;
        switch (temp)
        {
            case true:
                PlayerScore.text = (manager.PlayerScore / 1000000).ToString() + "m";
                break;
            case false:
                PlayerScore.text = (manager.PlayerScore / 1000).ToString() + "k";
                break;
            default:
                PlayerScore.text = manager.PlayerScore.ToString();
                break;
        }
    }
}
