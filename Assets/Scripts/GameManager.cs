using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public delegate void ScoreUp();
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Game Manager instance");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] int[] segments = new int[16];
    [SerializeField] private SegmentController[] segmentText;
    public ScoreUp ScoreUpCallBack;
    [SerializeField] private float playerScore = 0;
    [SerializeField] private GameObject Wheel;
    int counter = 0;

    private void Start()
    {
        SetSegmentsValue();
        LoadTheProgress();
    }

    public float PlayerScore
    {
        get { return playerScore; }
        set
        {
            playerScore += value;
            ScoreUpCallBack?.Invoke();
        }
    }

    void LoadTheProgress()
    {
        Progress progress = Saver.LoadTheProgress();
        if (progress != null)
        {
            Debug.Log("Progress Loaded");
            playerScore = progress.ScoreValue;
            ScoreUpCallBack?.Invoke();
        }
    }
    void SaveTheProgress()
    {
        Saver.SaveTheProgress();
    }

    public void SaveAndExit()
    {
        SaveTheProgress();
        SceneManager.LoadScene("MenuScene");
    }

    public void ClearResult()
    {
        playerScore = 0;
        ScoreUpCallBack?.Invoke();
    }

    public void LaunchWheel()
    {
        int random = Random.Range(1, 10) * 100;
        Wheel.GetComponent<Rigidbody>().AddTorque(Vector3.forward * random, ForceMode.Force);
    }

    void SetSegmentsValue()
    {
        for (int i = 0; i < segments.Length; i++)
        {
            segments[i] = GenerateUniqueValue();
            segmentText[i].SetValues(segments[i]);
        }
    }

    int GenerateUniqueValue()
    {

        int random = Random.Range(10, 1000) * 100;
        foreach (int j in segments)
        {
            if (j + 999 > random && j - 999 < random)
            {
                do
                {
                    counter++;
                    return GenerateUniqueValue();
                }
                while (counter < 20);
            }
        }
        counter = 0;
        return random;
    }

}
