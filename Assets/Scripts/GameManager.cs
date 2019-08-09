using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] private float playerScore = 0;

    [Header("Segment values and references")]
    [SerializeField] int[] segments = new int[16];
    [SerializeField] private SegmentController[] segmentText;
    [SerializeField] private GameObject WheelGameObject;

    [Header("Visual illusion of rotations")]
    [SerializeField] float FalseRotations;
    [SerializeField] float rotationDegrees;

    [Header("Angle of win position")]
    [SerializeField] private float winAngle;
    [SerializeField] private SegmentController WinPosition;
    

    public ScoreUp ScoreUpCallBack;
    float NumberOfRotations;
    RectTransform wheel;


    private void Start()
    {
        wheel = WheelGameObject.GetComponent<RectTransform>();
        GenerateNewSectionValue();
        LoadTheProgress();
    }

    SegmentController ChooseRandomSegment(SegmentController[] segments)
    {
        return segments[Random.Range(0, segments.Length)];
    }

    public void LaunchRotation()
    {
        WinPosition = ChooseRandomSegment(segmentText);
        winAngle = WinPosition.myAngle;
        // False Rotations Count + subtraction the current offset + Win Angle Value = full amount of degrees to turn the wheel;
        NumberOfRotations = (FalseRotations * 360) + (360 - wheel.transform.eulerAngles.z) + winAngle;
        StartCoroutine("LaunchTheWheel");
    }
    IEnumerator LaunchTheWheel()
    {
        for (float i = 0; i < NumberOfRotations; i += rotationDegrees)
        {
            wheel.Rotate(new Vector3(0, 0, rotationDegrees));
            yield return null;
        }
        PlayerScore = WinPosition.Value;
    }

    public float PlayerScore
    {
        get { return playerScore; }
        set
        {
            playerScore += value;
            Debug.Log("Added " + value);
            ScoreUpCallBack?.Invoke();
        }
    }

    void LoadTheProgress()
    {
        Progress progress = Saver.LoadTheProgress();
        if (progress != null)
        {
            playerScore = progress.ScoreValue;
            ScoreUpCallBack?.Invoke(); // update recordUI
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

    void GenerateNewSectionValue() // generate and save unique value for every section of wheel
    {
        for (int i = 0; i < segments.Length; i++)
        {
            segments[i] = GenerateUniqueValue();
            segmentText[i].SetValues(segments[i]);
        }
    }

    int counter = 0;
    int GenerateUniqueValue() // generate unique value with interval 1000 between other values
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
