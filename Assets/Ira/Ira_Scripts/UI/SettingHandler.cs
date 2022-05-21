using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingHandler : MonoBehaviour
{
    public static SettingHandler instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [Header("Buttons")]
    public Button MusicOn;
    public Button MusicOff;


    private void Start()
    {
        if (MenusHandler.instance.playerPrefsHandler.GetMusicStatus() == 0)
        {
            MusicOn.gameObject.SetActive(false);
            MusicOff.gameObject.SetActive(true);
        }
        else
        {
            MusicOn.gameObject.SetActive(true);
            MusicOff.gameObject.SetActive(false);
        }
    }

    public void OnCLoseButtonCLick()
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.genericButtonClickBack);
        MainMenuHandler.instance.CloseSettingPanel();
    }

    public void MusicSetting()
    {
        if (!MusicOn.gameObject.activeSelf)
        {
            MenusHandler.instance.playerPrefsHandler.SetMusicStatus(1);
            SoundManager.instance.MusicEnabled = true;
            SoundManager.instance.EnableSounds = true;
            SoundManager.instance.PlayBackgroundMusic(AudioClipsSource.Instance.MainMenuClip);
            MusicOn.gameObject.SetActive(true);
            MusicOff.gameObject.SetActive(false);
            SoundManager.instance.PlayEffect(AudioClipsSource.Instance.genericButtonClickBack);
        }
        else
        {
            
            MenusHandler.instance.playerPrefsHandler.SetMusicStatus(0);
            SoundManager.instance.MusicEnabled = false;
            SoundManager.instance.EnableSounds = false;
            SoundManager.instance.StopBackgroundMusic(AudioClipsSource.Instance.MainMenuClip);
            MusicOff.gameObject.SetActive(true);
            MusicOn.gameObject.SetActive(false);
            SoundManager.instance.PlayEffect(AudioClipsSource.Instance.GenericButtonClick2);
        }
        
    }


}
