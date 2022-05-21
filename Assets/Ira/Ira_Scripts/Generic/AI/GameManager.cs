using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public int LevelSelected = 0;
    public int SelectMode= 0;
    public int LevelSelectedM = 0;
    public bool fromGamePlay = false;
    public bool MainMenuToLoading = false;
    public bool FirstModeCompleteCheck;
    public PlayerPrefsHandler playerPrefsHandler;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void checkFirstModeComplete()
    {
        if (SelectMode == 0)
        {
            if (LevelSelected == 4 && FirstModeCompleteCheck)
            {
                playerPrefsHandler.SetGameMode(1);
                SelectMode = 1;
                playerPrefsHandler.SetLevelReachedNewMode(0);
            }
        }
        else
        {
            if (LevelSelected == 12)
            {
                FirstModeCompleteCheck = false;
                playerPrefsHandler.SetLevelReached(0);
                playerPrefsHandler.SetLevelReachedNewMode(0);
                playerPrefsHandler.SetGameMode(0);
            }
        }
    }
}
