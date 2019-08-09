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
        if (manager.PlayerScore <= 1000) PlayerScore.text = manager.PlayerScore.ToString(); //if less then 1000 (for example 0) do nothing
        else if (manager.PlayerScore > 1000 && manager.PlayerScore < 1000000)// if more than 1000 add k letter to the end
        {
            PlayerScore.text = (manager.PlayerScore / 1000).ToString() + "k";
        }
        else if (manager.PlayerScore >= 1000000)//if more than million, add m letter to the end
        {
            PlayerScore.text = (manager.PlayerScore / 1000000).ToString() + "m";
        }
    }
}
