using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPanelHandler : MonoBehaviour
{
   public void OnYesButtonClick()
   {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.genericButtonClickBack);
        Application.Quit();
   }
    public void OnCancelButtonClick()
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.genericButtonClickBack);
        MainMenuHandler.instance.CloseExitPanel();
    }
}
