using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    public static MainMenuHandler instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    private void Start()
    {
        GameManager.instance.checkFirstModeComplete();
        if (MenusHandler.instance.playerPrefsHandler.GetGameMode() == 0)
        {
            GameManager.instance.SelectMode = 0;
        }
        else
        {
            GameManager.instance.SelectMode = 1;
        }
        SoundManager.instance.PlayBackgroundMusic(AudioClipsSource.Instance.MainMenuClip);
    }

    public void OnClickNewStory() {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.GenericButtonClick);
        if (GameManager.instance.SelectMode ==0)
        {
            if (MenusHandler.instance.playerPrefsHandler.GetLevelReachedNewMode() > 0)
            {
                MenusHandler.instance.MainMenuPanel.SetActive(false);
                MenusHandler.instance.ResetPanel.SetActive(true);
            }
            else
            {
                GameManager.instance.LevelSelected = 0;
                MenusHandler.instance.playerPrefsHandler.SetLevelReachedNewMode(0);
                MenusHandler.instance.ResetPanel.SetActive(false);
                MenusHandler.instance.LoadingPanel.SetActive(true);
                MenusHandler.instance.Environment.SetActive(false);
                LoadingHandler.instance.Loading();
            }
        }
        else
        {
            if (MenusHandler.instance.playerPrefsHandler.GetLevelReached() > 0)
            {
                MenusHandler.instance.MainMenuPanel.SetActive(false);
                MenusHandler.instance.ResetPanel.SetActive(true);
            }
            else
            {
                GameManager.instance.LevelSelected = 0;
                MenusHandler.instance.playerPrefsHandler.SetLevelReached(0);
                MenusHandler.instance.ResetPanel.SetActive(false);
                MenusHandler.instance.LoadingPanel.SetActive(true);
                MenusHandler.instance.Environment.SetActive(false);
                LoadingHandler.instance.Loading();
            }
        }
        
        //MenusHandler.instance.Environment.SetActive(false);
    }

    public void OnPlayButtonClick()
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.GenericButtonClick);
        MenusHandler.instance.MainMenuPanel.SetActive(false);
        MenusHandler.instance.LoadingPanel.SetActive(true);
        LoadingHandler.instance.Loading();
        MenusHandler.instance.Environment.SetActive(false);
    }

    public void OnExitButtonClick()
    {
        SoundManager.instance.StopBackgroundMusic(AudioClipsSource.Instance.MainMenuClip);
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.genericButtonClickBack);
        MenusHandler.instance.MainMenuPanel.SetActive(false);
        MenusHandler.instance.ExitPanel.SetActive(true);
        MenusHandler.instance.ExitPanel.transform.GetChild(0).DOScale(1, 0.4f).SetEase(Ease.InSine).OnComplete(delegate
        {

        });
    }

    public void OnRateUsButtonClick()
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.genericButtonClickBack);
        Application.OpenURL(MenusHandler.instance.RateUsLink);
    }


    public void CloseExitPanel()
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.genericButtonClickBack);
        MenusHandler.instance.ExitPanel.transform.GetChild(0).DOScale(0, 0.4f).SetEase(Ease.OutSine).OnComplete(delegate
        {
            MenusHandler.instance.ExitPanel.SetActive(false);
            MenusHandler.instance.MainMenuPanel.SetActive(true);
            SoundManager.instance.PlayBackgroundMusic(AudioClipsSource.Instance.MainMenuClip);
        });
    }

    public void OnSettingButtonClick()
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.genericButtonClickBack);
        MenusHandler.instance.MainMenuPanel.SetActive(false);
        MenusHandler.instance.SettingPanel.SetActive(true);
        MenusHandler.instance.SettingPanel.transform.GetChild(0).DOScale(1, 0.4f).SetEase(Ease.InSine).OnComplete(delegate
        {

        });
    }

    public void CloseSettingPanel()
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.genericButtonClickBack);
        MenusHandler.instance.SettingPanel.transform.GetChild(0).DOScale(0, 0.4f).SetEase(Ease.OutSine).OnComplete(delegate
        {
            MenusHandler.instance.SettingPanel.SetActive(false);
            MenusHandler.instance.MainMenuPanel.SetActive(true);
        });
    }
}
