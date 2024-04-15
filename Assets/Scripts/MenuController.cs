using UnityEngine;
using UnityEngine.Audio;
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
    bool windowed = false;

    public AudioSource audio;
    public AudioSource bgm;

    [SerializeField] AudioMixer masterMixer;

    static int[] option1 = new int[2] { 640, 480 };
    static int[] option2 = new int[2] { 800, 800 };
    static int[] option3 = new int[2] { 1024, 768 };

    int[][] resolutions = new int[][] { option1, option2, option3 };

    // Start is called before the first frame update
    void Start()
    {
        w = resolutions[2][0];
        h = resolutions[2][1];

        bgm.Play();
        setCanvasHome();
    }

    public void startGame()
    {
        Debug.Log("START");
        SceneManager.LoadScene("SampleScene");
    }
    
    public void setCanvasHome()
    {
        audio.Play();
        home.gameObject.SetActive(true);
        options.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        Debug.Log("SET HOME");
    }

    public void setCanvasOptions()
    {
        audio.Play();
        home.gameObject.SetActive(false);
        options.gameObject.SetActive(true);
        credits.gameObject.SetActive(false);
        Debug.Log("SET OPT");
    }

    public void setCanvasCredits()
    {
        audio.Play();
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
        audio.Play();

        w = resolutions[resolutionDropdown.value][0];
        h = resolutions[resolutionDropdown.value][1];

        Screen.SetResolution(w, h, windowed);
        Debug.Log(resolutionDropdown.value + " " + w + " " + h);
    }

    public void sliderChange()
    {
        Debug.Log(slider.value);

        float volume = 20f;

        volume = volume > 0 ? volume * slider.value - 20f : 0;

        masterMixer.SetFloat("MasterVolume", volume);
    }

    public void fullscreenToggle()
    {
        audio.Play();
        Debug.Log(fullScreenToggler.isOn);
        Screen.SetResolution(w,h, windowed);
    }

    // Update is called once per frame
    void Update()
    {

    }

 }
