using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    #region Varaibles
    [SerializeField]
    private Button musicBtn;

    [SerializeField]
    private Sprite soundOff, SoundOn;
    #endregion

    public void PlayGame()
    {
        GameManager.instance.gameStartedFromMainMenu = true;
        SceneManager.LoadScene(Tags.GAMEPLAY_SCENE);

    }

    public void ControlMusic()
    {
        if(GameManager.instance.canPlayMusic)
        {
            musicBtn.image.sprite = SoundOn;
            GameManager.instance.canPlayMusic = false;
        }
        else
        {
            musicBtn.image.sprite = soundOff;
            GameManager.instance.canPlayMusic = true;
        }
    }





} // MainMenuController class
