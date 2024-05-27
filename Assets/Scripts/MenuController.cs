using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Canvas home;
    public Canvas options;
    public Canvas credits;
    public Dropdown resolutionDropDown;
    public Slider slider;
    public Toggle fullScreenToggler;
    public Toggle screenSizeToggler;
    private FullScreenMode fullScreenMode;
    //int h, w;
    bool full = true;

    public AudioSource myaudio;
    public AudioSource bgm;

    public AudioMixer masterMixer;

    Resolution[] resolutions;

    [SerializeField] private GameObject transitionScreen;

    // Start is called before the first frame update
    //void Start()
    //{
    //    resolutions = Screen.resolutions;

    //    w = resolutions[resolutions.Count() -1].width;
    //    h = resolutions[resolutions.Count() -1].height;

    //    Screen.SetResolution(w, h, full);

    //    bgm.Play();
    //    setCanvasHome();

    //    masterMixer.SetFloat("MasterVolume", 0f);

    //    // Create a list to store resolution strings
    //    List<string> options = new List<string>();

    //    foreach (var res in resolutions)
    //    {

    //        if (res.width >= 640)
    //        {
    //            string option = res.width + " x " + res.height;

    //            Debug.Log(option);

    //            if(!options.Contains(option)) options.Add(option);
    //        }
    //    }

    //    resolutionDropdown.AddOptions(options);
    //    transitionScreen.SetActive(false);
    //}

    private void Start()
    {

        fullScreenMode = FullScreenMode.ExclusiveFullScreen; 

        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " " + (int)resolutions[i].refreshRateRatio.value + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }


    public void startGame()
    {
        Debug.Log("START");
        home.gameObject.SetActive(false);
        transitionScreen.SetActive(true);
        SceneManager.LoadScene("PlayTest");
    }

    public void setCanvasHome()
    {
        myaudio.Play();
        home.gameObject.SetActive(true);
        options.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        Debug.Log("SET HOME");
    }

    public void setCanvasOptions()
    {
        myaudio.Play();
        home.gameObject.SetActive(false);
        options.gameObject.SetActive(true);
        credits.gameObject.SetActive(false);
        Debug.Log("SET OPT");
    }

    public void setCanvasCredits()
    {
        myaudio.Play();
        home.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        credits.gameObject.SetActive(true);
        Debug.Log("SET CRED");
    }

    public void exitGame()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }

    //public void resolutionChange()
    //{
    //    myaudio.Play();

    //    w = resolutions[resolutionDropdown.value].width;
    //    h = resolutions[resolutionDropdown.value].height;

    //    Screen.SetResolution(w, h, full);
    //    Debug.Log(resolutionDropdown.value + " " + w + " " + h);
    //}

    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fullScreenMode, resolution.refreshRateRatio);
    }

    public void sliderChange()
    {
        Debug.Log("Volume " + slider.value + " Final " + Mathf.Log10(slider.value * 20));
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(slider.value * 20));
    }

    public void fullscreenToggle()
    {
        myaudio.Play();
        Debug.Log(fullScreenToggler.isOn);

        if (full) fullScreenMode = FullScreenMode.FullScreenWindow;
        else fullScreenMode = FullScreenMode.ExclusiveFullScreen;

        full = !full;
        //Screen.SetResolution(w, h, full);
    }

}
