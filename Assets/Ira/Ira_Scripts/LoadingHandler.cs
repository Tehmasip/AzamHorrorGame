using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingHandler : MonoBehaviour
{

    public static LoadingHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private float _progress;
    public Image LoadingBar;

    public void Loading()
    {
        StartCoroutine(LoadScene());//Start the Coroutine at LoadScene IEnumerator which will calculate the loading bar and then change scene to GamePlay 
    }
    IEnumerator LoadScene()
    {
        AsyncOperation asyncOperation;
        if (GameManager.instance.SelectMode == 0)
        {
            asyncOperation = SceneManager.LoadSceneAsync(StaticVariables.FirstScene);//Starts the operation of loading the GamePlay
        }
        else
        {
            asyncOperation = SceneManager.LoadSceneAsync(StaticVariables.GamePlayMenu);//Starts the operation of loading the GamePlay
        }

        ///AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(StaticVariables.GamePlayMenu);//Starts the operation of loading the GamePlay

        while (!asyncOperation.isDone)// This Loop continues utill the operation of loading GamePlay scene is not done!!
        {
            _progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);//We are Clamping the progress of loading Gameplay from 0 to 1
            LoadingBar.fillAmount = _progress;//Giving the progress to loading bar            
            yield return null;
        }
    }

}
