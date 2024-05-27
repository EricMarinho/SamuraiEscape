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

    [Header("KunaiIndicator")]
    [SerializeField] private Image kunaiIndicator;

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
                AudioManager.instance.UnmuteSounds();
                toggleLabel.text = "Mute";
                
            }
            else
            {
                AudioManager.instance.MuteSounds();
                toggleLabel.text = "Unmute";
            }
        });
        GameEvents.Instance.OnTutorialTriggerEntered += (message) =>
        {
            tutorialPopUpText.text = message;
            tutorialPopup.SetActive(true);
            closeTutorialButton.gameObject.SetActive(true);
            Time.timeScale = 0;
            AudioManager.instance.StopWalkingSound();
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
            toggleMuteButton.gameObject.SetActive(false);
            openReturnToMenuButtonPopup.gameObject.SetActive(false);
        };
        endGameReturnToMenuButton.onClick.AddListener(() =>
        {
            endGameScreen.SetActive(false);
            SceneManager.LoadScene("HomeScreen");
        });
        GameEvents.Instance.OnKunaiDisable += () =>
        {
            kunaiIndicator.color = new Color(kunaiIndicator.color.r, kunaiIndicator.color.g, kunaiIndicator.color.b, 0.3f);
        };
        GameEvents.Instance.OnKunaiRecovered += () =>
        {
            kunaiIndicator.color = new Color(kunaiIndicator.color.r, kunaiIndicator.color.g, kunaiIndicator.color.b, 1f);
        };
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
