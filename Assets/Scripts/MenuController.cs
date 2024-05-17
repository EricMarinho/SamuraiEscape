using System.Collections.Generic;
using System.Linq;
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
    public Dropdown resolutionDropdown;
    public Slider slider;
    public Toggle fullScreenToggler;
    public Toggle screenSizeToggler;

    int h, w;
    bool full = true;

    public AudioSource myaudio;
    public AudioSource bgm;

    public AudioMixer masterMixer;

    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;

        w = resolutions[resolutions.Count() -1].width;
        h = resolutions[resolutions.Count() -1].height;

        Screen.SetResolution(w, h, full);

        bgm.Play();
        setCanvasHome();

        masterMixer.SetFloat("MasterVolume", 0f);

        // Create a list to store resolution strings
        List<string> options = new List<string>();

        foreach (var res in resolutions)
        {

            if (res.width >= 640)
            {
                string option = res.width + " x " + res.height;

                Debug.Log(option);

                if(!options.Contains(option)) options.Add(option);
            }
        }

        resolutionDropdown.AddOptions(options);
    }

    public void startGame()
    {
        Debug.Log("START");
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

    public void resolutionChange()
    {
        myaudio.Play();

        w = resolutions[resolutionDropdown.value].width;
        h = resolutions[resolutionDropdown.value].height;

        Screen.SetResolution(w, h, full);
        Debug.Log(resolutionDropdown.value + " " + w + " " + h);
    }

    public void sliderChange()
    {

        float vol = 40f;

        vol = slider.value > 0.1f ? vol * slider.value - 40f : -80f;

        Debug.Log("Slider: " + slider.value + "Volume: " + vol);

        masterMixer.SetFloat("MasterVolume", vol);
    }

    public void fullscreenToggle()
    {
        myaudio.Play();
        Debug.Log(fullScreenToggler.isOn);

        full = !full;
        Screen.SetResolution(w,h, full);
    }

    // Update is called once per frame
    void Update()
    {

    }

 }
