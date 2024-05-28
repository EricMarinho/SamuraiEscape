using System;
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

    int resolutionIdx;

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
        Time.timeScale = 1;

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
        currentResolutionIndex = PlayerPrefs.GetInt("Resolution", resolutionIdx);

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;

        SetResolution();
        SetVolume();

        resolutionDropDown.RefreshShownValue();
    }

    private void SetResolution()
    {
        resolutionIdx = PlayerPrefs.HasKey("Resolution") ? PlayerPrefs.GetInt("Resolution") : resolutions.Length - 1;
        Resolution resolution = resolutions[resolutionIdx];
        Screen.SetResolution(resolution.width, resolution.height, full);
    }

    private void SetVolume()
    {
        float volume = PlayerPrefs.HasKey("Volume") ? float.Parse(PlayerPrefs.GetString("Volume")) : 1f;
        slider.value = volume;
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(volume + 0.0001f) * 20);
    }

    public void startGame()
    {
        Debug.Log("START");
        home.gameObject.SetActive(false);
        transitionScreen.SetActive(true);
        SceneManager.LoadScene("IntroductionScene");
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

    public void setResolution()
    {
        myaudio.Play();

        resolutionIdx = resolutionDropDown.value;
        Resolution resolution = resolutions[resolutionIdx];

        Debug.Log(resolution);

        Screen.SetResolution(resolution.width, resolution.height, full);
        PlayerPrefs.SetInt("Resolution", resolutionIdx);
        PlayerPrefs.Save();

    }

    public void sliderChange()

    {
        //float vol = 40f;

        //vol = slider.value > 0.1f ? vol * slider.value - 40f : -80f;

        //Debug.Log("Slider: " + slider.value + "Volume: " + vol);

        //masterMixer.SetFloat("MasterVolume", vol);
        
        Debug.Log("Volume " + slider.value + " Final " + Mathf.Log10(slider.value * 20));
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(slider.value + 0.0001f) * 20);
        PlayerPrefs.SetString("Volume", slider.value.ToString());
        PlayerPrefs.Save();
    }

    public void fullscreenToggle()
    {
        myaudio.Play();
        Debug.Log(fullScreenToggler.isOn);

        full = !full;

        resolutionIdx = resolutionDropDown.value;
        Resolution resolution = resolutions[resolutionIdx];

        Screen.SetResolution(resolution.width, resolution.height, full);
    }

}
