using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance;

    public bool gameStartedFromMainMenu, gameRestartedPlayerDied;

    [HideInInspector]
    public float score, health, level;

    [HideInInspector]
    public bool canPlayMusic = true;

    #endregion

    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }


} // GameManager class
