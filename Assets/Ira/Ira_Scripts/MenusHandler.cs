using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenusHandler : MonoBehaviour
{
    public static MenusHandler instance;

    public PlayerPrefsHandler playerPrefsHandler;

    public Text QuestNumber;
    public Text DayNumber;
    public Button ContinueButton;
    public GameObject Environment;

    public bool IsStarted=false;

    public Scrollbar Senstivityscrollbar;

    public string RateUsLink;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (playerPrefsHandler.GetStartGameForFirstTime()==0)
        {
            TapToStartPanel.SetActive(true);
            playerPrefsHandler.SetStartGameForFirstTime(1);
        }
        else
        {
            TapToStartPanel.SetActive(false);
            MainMenuPanel.SetActive(true);
        }

    }

    public void ONSestivityBarValChange()
    {
        playerPrefsHandler.SetSenstivity(Senstivityscrollbar.value);
    }

    public void TouchToStart()
    {
        if(!IsStarted)
        {
            IsStarted = true;
            FadeEffect.SetActive(true);
        }

    }
    private void Start()
    {
        // DayNumber.text = playerPrefsHandler.GetDay().ToString();
        if (GameManager.instance.SelectMode ==0)
        {
            GameManager.instance.LevelSelected = playerPrefsHandler.GetLevelReachedNewMode();
            if (GameManager.instance.LevelSelected == 0 )
            {
                ContinueButton.interactable = false;
            }
        }
        else
        {
            if (GameManager.instance.LevelSelected == 12)
            {
                ContinueButton.interactable = false;

                GameManager.instance.FirstModeCompleteCheck = false;
                GameManager.instance.playerPrefsHandler.SetLevelReached(0);
                GameManager.instance.playerPrefsHandler.SetLevelReachedNewMode(0);
                GameManager.instance.playerPrefsHandler.SetGameMode(0);
                GameManager.instance.SelectMode = 0;

            }
            GameManager.instance.LevelSelected = playerPrefsHandler.GetLevelReached();
        }
        
        Senstivityscrollbar.value = playerPrefsHandler.GetSenstivity();
        int temp = GameManager.instance.LevelSelected + 1;
        QuestNumber.text = temp.ToString();
        DayNumber.text = temp.ToString();
        if (GameManager.instance.LevelSelected == 0|| GameManager.instance.LevelSelected == 12)
        {
            
        }
        else
        {
            ContinueButton.interactable = true;
        }

        if (playerPrefsHandler.GetMusicStatus() == 0)
        {
            SoundManager.instance.MusicEnabled = false;
            SoundManager.instance.EnableSounds = false;
        }
        else
        {
            SoundManager.instance.MusicEnabled = true;
            SoundManager.instance.EnableSounds = true;
        }

    }

    public GameObject MainMenuPanel;
    public GameObject LoadingPanel;
    public GameObject ResetPanel;
    public GameObject ExitPanel;
    public GameObject SettingPanel;
    public GameObject TapToStartPanel;
    public GameObject FadeEffect;
}
