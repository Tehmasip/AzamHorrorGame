using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour
{
    #region Singleton
    public static TimeHandler Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }
    #endregion

    private float SecondsOfTimer;//Seconds of Timer
    private float MinutesOfTimer;//Minutes of Timer
    public float _timerStart;//Total TIme of Timer

    public Text TimerMinuteValue;
    public Text TimerSecondsValue;

    //public bool WatchVideoFlag = false;
    public bool Chk = true;
    
    private void Start()
    {
        ResetTimer();
    }

    public void ResetTimer()
    {
        SecondsToMinutesAndSeconds();
        AssigneMinutesAndSecondsToTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Chk)
        {
            Timer();
            if ((int)SecondsOfTimer == 0 && (int)MinutesOfTimer == 0 &&!UIManager.Instance.IsWatchVideoSkip)
            {
                UIManager.Instance.OnWatchVideoSkipButtonClick();
            }
        }
    }
    void SecondsToMinutesAndSeconds()
    {
        //_timerStart = GamePlay.Instance.AllLevels[GameManager.Instance.LevelSelected].Timer;
        SecondsOfTimer = Mathf.Round(_timerStart % 60);
        MinutesOfTimer = (int)(_timerStart / 60);
    }
    void AssigneMinutesAndSecondsToTimer()
    {
        TimerSecondsValue.text = ":"+SecondsOfTimer.ToString();
        TimerMinuteValue.text = MinutesOfTimer.ToString();
    }
    void Timer()
    {
        if (SecondsOfTimer > 0)
        {
            SecondsOfTimer -= Time.deltaTime;

        }
        else
        {
            if (MinutesOfTimer > 0)
            {
                MinutesOfTimer--;
                SecondsOfTimer = 59;
            }

        }

        if (Mathf.Round(MinutesOfTimer) < 10)
        {
            TimerMinuteValue.text = "0" + Mathf.Round(MinutesOfTimer).ToString();
        }
        else
        {
            TimerMinuteValue.text = Mathf.Round(MinutesOfTimer).ToString();
        }
        if (Mathf.Round(SecondsOfTimer) < 10)
        {
            TimerSecondsValue.text = ":0" + Mathf.Round(SecondsOfTimer).ToString();
        }
        else
        {
            TimerSecondsValue.text = ":"+Mathf.Round(SecondsOfTimer).ToString();
        }
    }
    public int MinutesToSeconds()
    {
        MinutesOfTimer *= 60;
        SecondsOfTimer += MinutesOfTimer;
        return (int)SecondsOfTimer;
    }
}
