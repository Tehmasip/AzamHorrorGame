using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinPanelHandler : MonoBehaviour
{
    public void onClickHome()
    {
        GameManager.instance.checkFirstModeComplete();
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.genericButtonClickBack);
        Time.timeScale = 1;
        
        SceneManager.LoadScene("Ira_MainMenu");
    }
    public void onClickReplay()
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.GenericButtonClick);
        Time.timeScale = 1;
        //GamePlayHandler.Instance.playerPrefsHandler.SetLevelReached(GamePlayHandler.Instance.playerPrefsHandler.GetLevelReached() - 1);
        //GameManager.instance.LevelSelected = GamePlayHandler.Instance.playerPrefsHandler.GetLevelReached();
        if (GameManager.instance.SelectMode == 0)
        {
            if (GameManager.instance.LevelSelected != 0 )
            {
                //GameManager.instance.LevelSelected -= GameManager.instance.LevelSelected;
                SceneManager.LoadScene("hroorr jangle");
            }
            else
            {
               // GameManager.instance.LevelSelected =0;
                SceneManager.LoadScene("hroorr jangle");
            }
        }
        else
        {
            SceneManager.LoadScene("ira_Hospital_Scene");
        }
        
    }
    public void onclickNextBtn()
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.GenericButtonClick);
        Time.timeScale = 1;
        if (GameManager.instance.SelectMode == 0)
        {
            if (GameManager.instance.LevelSelected == 4 && GameManager.instance.FirstModeCompleteCheck)
            {
                GamePlayHandler.Instance.playerPrefsHandler.SetGameMode(1);
                GameManager.instance.SelectMode = 1;
                GamePlayHandler.Instance.playerPrefsHandler.SetLevelReachedNewMode(0);
            }
        }

        if (GameManager.instance.SelectMode ==0)
        {
            GameManager.instance.LevelSelected += 1;
            SceneManager.LoadScene("hroorr jangle");
        }
        else
        {
            GameManager.instance.LevelSelected = GamePlayHandler.Instance.playerPrefsHandler.GetLevelReached();
            SceneManager.LoadScene("ira_Hospital_Scene");
        }
        
    }
}
