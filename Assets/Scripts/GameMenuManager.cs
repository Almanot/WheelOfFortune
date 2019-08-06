using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField]private string PlaySceneName;
    Resolution[] resolutions;

    public Dropdown ResolutionsDropdown;

    // Use this for initialization
    void Start()
    {
        int currentResolutionIndex = 0;
        resolutions = Screen.resolutions;

        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);

            if (resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionsDropdown.ClearOptions();
        ResolutionsDropdown.AddOptions(options);
        ResolutionsDropdown.value = currentResolutionIndex;
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetResolution(int index)
    {

        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreen);
    }

    public void SetFullScreen(bool value)
    {
        Screen.fullScreen = value;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(PlaySceneName);
    }
}
