using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    [Header("Return to Menu")]
    [SerializeField] private GameObject returnToMenuPopup;
    [SerializeField] private Button openReturnToMenuButtonPopup;
    [SerializeField] private Button closeReturnToMenuButtonPopup;
    [SerializeField] private Button returnToMenuButton;

    [Header("Audio")]
    [SerializeField] private Toggle toggleMuteButton;
    [SerializeField] private Text toggleLabel;

    [Header("Tutorial")]
    [SerializeField] private GameObject tutorialPopup;
    [SerializeField] private Text tutorialPopUpText;
    [SerializeField] private Button closeTutorialButton;

    [Header("End Game")]
    [SerializeField] private GameObject endGameScreen;
    [SerializeField] private Button endGameReturnToMenuButton;

    private void Start()
    {
        openReturnToMenuButtonPopup.onClick.AddListener(() =>
        {
            returnToMenuPopup.SetActive(true);
            Time.timeScale = 0;
        });
        closeReturnToMenuButtonPopup.onClick.AddListener(() =>
        {
            returnToMenuPopup.SetActive(false);
            Time.timeScale = 1;
        });
        returnToMenuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("HomeScreen");
        });
        toggleMuteButton.onValueChanged.AddListener((value) =>
        {
            if (value)
            {
                AudioManager.instance.audioSource.volume = PlayerPrefs.GetInt("Volume", 1);
                toggleLabel.text = "Mute";
                
            }
            else
            {
                AudioManager.instance.audioSource.volume = 0;
                toggleLabel.text = "Unmute";
            }
        });
        GameEvents.Instance.OnTutorialTriggerEntered += (message) =>
        {
            tutorialPopUpText.text = message;
            tutorialPopup.SetActive(true);
            closeTutorialButton.gameObject.SetActive(true);
            Time.timeScale = 0;
        };
        closeTutorialButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            tutorialPopup.SetActive(false);
            closeTutorialButton.gameObject.SetActive(false);
        });
        GameEvents.Instance.OnEndGame += () =>
        {
            endGameScreen.SetActive(true);
        };
        endGameReturnToMenuButton.onClick.AddListener(() =>
        {
            endGameScreen.SetActive(false);
            SceneManager.LoadScene("HomeScreen");
        });
    }

    private void OnDestroy()
    {
        openReturnToMenuButtonPopup.onClick.RemoveAllListeners();
        closeReturnToMenuButtonPopup.onClick.RemoveAllListeners();
        returnToMenuButton.onClick.RemoveAllListeners();
        toggleMuteButton.onValueChanged.RemoveAllListeners();
        closeTutorialButton.onClick.RemoveAllListeners();
        endGameReturnToMenuButton.onClick.RemoveAllListeners();

        GameEvents.Instance.OnTutorialTriggerEntered -= (message) =>
        {
            tutorialPopUpText.text = message;
            tutorialPopup.SetActive(true);
            closeTutorialButton.gameObject.SetActive(true);
            Time.timeScale = 0;
        };
        GameEvents.Instance.OnEndGame -= () =>
        {
            endGameScreen.SetActive(true);
        };
    }

    public void CloseReturnToMenuPopup()
    {
        returnToMenuPopup.SetActive(false);
    }
}
