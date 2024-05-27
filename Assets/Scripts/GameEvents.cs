using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    //singleton
    public static GameEvents Instance;

    public Action OnCrystalCollected;
    public Action OnCrystalRestored;
    public Action OnEndGame;
    public Action OnKunaiDisable;
    public Action OnKunaiRecovered;
    public Action<string> OnTutorialTriggerEntered;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
