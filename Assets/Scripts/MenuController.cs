using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Canvas home;
    public Canvas options;
    public Canvas credits;
    public Dropdown resolutionDropdown;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        setCanvasHome();
    }

    enum enumResolution
    {
        option1,
        option2,
        option
    }

    static int[] option1 = new int[2] { 640, 480 };
    static int[] option2 = new int[2] { 800, 800 };
    static int[] option3 = new int[2] { 1024, 768 };

    int[][] resolutions = new int[][] {option1, option2, option3} ;

    public void startGame()
    {
        Debug.Log("START");
    }

    public void setCanvasHome()
    {
        home.gameObject.SetActive(true);
        options.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        Debug.Log("SET HOME");
    }

    public void setCanvasOptions()
    {
        home.gameObject.SetActive(false);
        options.gameObject.SetActive(true);
        credits.gameObject.SetActive(false);
        Debug.Log("SET OPT");
    }

    public void setCanvasCredits()
    {
        home.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        credits.gameObject.SetActive(true);
        Debug.Log("SET CRED");
    }

    public void exitGame()
    {
        Debug.Log("EXIT");
    }

    public void resolutionChange()
    {
        Debug.Log(resolutionDropdown.value);
    }

    public void sliderChange()
    {
        Debug.Log(slider.value);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
