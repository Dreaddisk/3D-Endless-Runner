﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{

    #region Variables
    public static GameplayController instance;

    [SerializeField]
    private AudioSource audioSource;

    [HideInInspector]
    public bool canCountScore;

    private Text scoreText, healthText, levelText;

    private float score, health, level;

    private BGScroller bgScroller;

    private GameObject pausePanel;
    #endregion


    #region UnityFunctions
    private void Awake()
    {
        MakeInstance();

        scoreText = GameObject.Find(Tags.SCORE_TEXT_OBJ).GetComponent<Text>();
        healthText = GameObject.Find(Tags.HEALTH_TEXT_OBJ).GetComponent<Text>();
        levelText = GameObject.Find(Tags.LEVEL_TEXT_OBJ).GetComponent<Text>();

        bgScroller = GameObject.Find(Tags.BACKGROUND_GAME_OBJ).GetComponent<BGScroller>();

        pausePanel = GameObject.Find(Tags.PAUSE_PANEL_OBJ);
        pausePanel.SetActive(false);
    }

    private void Start()
    {
        if(GameManager.instance.canPlayMusic)
        {
            audioSource.Play();
        }
    }

    private void Update()
    {
        IncrementScore(1);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneWasLoaded;
        instance = null;
    }

    #endregion

    void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == Tags.GAMEPLAY_SCENE)
        {
            if(GameManager.instance.gameStartedFromMainMenu)
            {
                GameManager.instance.gameStartedFromMainMenu = false;
                score = 0;
                health = 3;
                level = 0;
            }
            else if(GameManager.instance.gameRestartedPlayerDied)
            {
                GameManager.instance.gameRestartedPlayerDied = false;
                score = GameManager.instance.score;
                health = GameManager.instance.health;
            }

            scoreText.text = score.ToString();
            healthText.text = health.ToString();
            levelText.text = level.ToString();

        }
    }

    public void TakeDamage()
    {
        health--;

        if(health >= 0)
        {
            StartCoroutine(PlayerDied(Tags.GAMEPLAY_SCENE));
            healthText.text = health.ToString();
        }
        else
        {
            StartCoroutine(PlayerDied(Tags.MAINMENU_SCENE));
        }
    }

    public void IncrementHealth()
    {
        health++;
        healthText.text = health.ToString();
    }


    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void IncrementScore(float scoreValue)
    {
        if(canCountScore)
        {
            score += scoreValue;
            scoreText.text = score.ToString();
        }
    }

    IEnumerator PlayerDied(string sceneName)
    {
        canCountScore = false;
        bgScroller.canScroll = false;

        GameManager.instance.score = score;
        GameManager.instance.health = health;
        GameManager.instance.gameRestartedPlayerDied = true;

        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(sceneName);
    }

    public void PauseGame()
    {
        canCountScore = false;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        canCountScore = true;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Tags.MAINMENU_SCENE);
    }

    public void Reload()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Tags.GAMEPLAY_SCENE);
    }





} // GameplayController class
