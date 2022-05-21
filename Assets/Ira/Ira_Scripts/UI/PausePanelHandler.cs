using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PausePanelHandler : MonoBehaviour
{
    public void onClickHome()
    {
        GameManager.instance.checkFirstModeComplete();
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.GenericButtonClick);
        Time.timeScale = 1;
        
        SceneManager.LoadScene("Ira_MainMenu");
    }
    public void onClickReStart()
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.GenericButtonClick);
        Time.timeScale = 1;
        if (GameManager.instance.SelectMode == 0)
        {
            SceneManager.LoadScene("hroorr jangle");
        }
        else
        {
            SceneManager.LoadScene("ira_Hospital_Scene");
        }
    }
    public void onClickResume() {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.GenericButtonClick);
        Time.timeScale = 1;
        UIManager.Instance.PausePanel.SetActive(false);
    }
}
