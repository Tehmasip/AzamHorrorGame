using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFadeHandler : MonoBehaviour
{
   public void OnFadeCompleted()
   {
        MenusHandler.instance.TapToStartPanel.SetActive(false);
        MenusHandler.instance.MainMenuPanel.SetActive(true);
        this.gameObject.SetActive(false);
   }
}
