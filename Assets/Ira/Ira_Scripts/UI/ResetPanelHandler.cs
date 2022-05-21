using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPanelHandler : MonoBehaviour
{
    public void OnClickCancel() {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.genericButtonClickBack);
        MenusHandler.instance.MainMenuPanel.SetActive(true);
        MenusHandler.instance.ResetPanel.SetActive(false);
    }

    public void OnClickYes() {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.GenericButtonClick);
        GameManager.instance.LevelSelected = 0;
        GameManager.instance.SelectMode = 0;
        MenusHandler.instance.playerPrefsHandler.SetLevelReached(0);
        MenusHandler.instance.playerPrefsHandler.SetLevelReachedNewMode(0);
        MenusHandler.instance.ResetPanel.SetActive(false);
        MenusHandler.instance.LoadingPanel.SetActive(true);
        MenusHandler.instance.Environment.SetActive(false);
        LoadingHandler.instance.Loading();
        
    }

}
