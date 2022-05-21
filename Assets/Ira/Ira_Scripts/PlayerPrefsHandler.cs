using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPrefsHandler", menuName = "Create Prefs Handler", order = 1)]

public class PlayerPrefsHandler : ScriptableObject
{
    public int LevelCompleted = 0;
    public int LevelCompletedNewMode = 0;
    public bool IsRemoveAds = false;

    public void SetLevelReached(int val)
    {
        PlayerPrefs.SetInt(StaticVariables.LevelReached, val);
    }


    public int GetLevelReached()
    {
        return PlayerPrefs.GetInt(StaticVariables.LevelReached);
    }
    public void SetLevelReachedNewMode(int val)
    {
        PlayerPrefs.SetInt(StaticVariables.LevelReachedNewMode, val);
    }

    public int GetLevelReachedNewMode()
    {
        return PlayerPrefs.GetInt(StaticVariables.LevelReachedNewMode);
    }

    public void SetGameMode(int val)
    {
        PlayerPrefs.SetInt(StaticVariables.SelectedMode, val);
    }

    public int GetGameMode()
    {
        return PlayerPrefs.GetInt(StaticVariables.SelectedMode);
    }


    public void SetStoryActivator(int val)
    {
        PlayerPrefs.SetInt(StaticVariables.StoryActivator, val);
    }

    public int GetStoryActivator()
    {
        return PlayerPrefs.GetInt(StaticVariables.StoryActivator, 0);
    }

    public void SetRemoveAd(int val)
    {
        PlayerPrefs.SetInt("RemoveAds", val);
    }

    public int GetRemoveAd()
    {
        return PlayerPrefs.GetInt("RemoveAds", 0);
    }

    public int GetMusicStatus()
    {
        return PlayerPrefs.GetInt("MusicStatus", 1);
    }

    public void SetMusicStatus(int n)
    {
        PlayerPrefs.SetInt("MusicStatus", n);
    }

    public int GetStartGameForFirstTime()
    {
        return PlayerPrefs.GetInt("PlayFirstTime", 0);
    }

    public void SetStartGameForFirstTime(int n)
    {
        PlayerPrefs.SetInt("PlayFirstTime", n);
    }

    public void SetSenstivity(float n)
    {
        PlayerPrefs.SetFloat("SetSenstivity", n);
    }

    public float GetSenstivity()
    {
        return PlayerPrefs.GetFloat("SetSenstivity", 0.5f);
    }

    public void SetDay(int n)
    {
        PlayerPrefs.SetInt("SetDayNumber", n);
    }

    public int GetDay()
    {
        return PlayerPrefs.GetInt("SetDayNumber", 1);
    }

}
