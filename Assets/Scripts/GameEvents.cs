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
