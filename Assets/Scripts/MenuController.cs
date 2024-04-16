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

    static int[] option1 = new int[2] { 800, 600 };
    static int[] option2 = new int[2] { 1024, 768 };
    static int[] option3 = new int[2] { 1920, 1080};

    int[][] resolutions = new int[][] { option1, option2, option3 };

    // Start is called before the first frame update
    void Start()
    {
        w = resolutions[2][0];
        h = resolutions[2][1];

        Screen.SetResolution(w, h, full);

        bgm.Play();
        setCanvasHome();

        masterMixer.SetFloat("MasterVolume", 0f);
    }

    public void startGame()
    {
        Debug.Log("START");
        SceneManager.LoadScene("SampleScene");
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

        w = resolutions[resolutionDropdown.value][0];
        h = resolutions[resolutionDropdown.value][1];

        Screen.SetResolution(w, h, full);
        Debug.Log(resolutionDropdown.value + " " + w + " " + h);
    }

    public void sliderChange()
    {

        float vol = 20f;

        vol = slider.value > 0.1f ? vol * slider.value - 20f : -80f;

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
