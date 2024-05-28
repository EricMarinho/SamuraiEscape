using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroductionClick : MonoBehaviour
{
    private void OnMouseUp()
    {
        SceneManager.LoadScene("PlayTest");
    }
}
