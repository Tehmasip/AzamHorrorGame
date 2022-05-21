using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Playables;

public class UIManager : MonoBehaviour
{
    //Singleton of GamePlay Class
    #region Singleton
    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion
    [Header("Images")]
    public Image CrossHairImage;
    public Image FillImage;
    public GameObject PlayerfaintEffect;
    public Image KeyImage;
    public GameObject CinematicBars;
    public GameObject DoctorCam;

    [Header("Buttons")]
    public Button PickButton;
    public Button InteractableButton;
    public Button UnEquipButton;
    public Button HintButton;
    public Button Skip;
    public Button ObjectiveButton;
    public Button SkipButton;
    public Button NewChapter;

    [Header("Panel")]
    public GameObject InventoryPanel;
    public GameObject WinLevelPanel;
    public GameObject FailLevelPanel;
    public GameObject ObjectivePanel;
    public GameObject HintPanel;
    public GameObject HealthBar;
    public GameObject PausePanel;
    public GameObject StoryPanel;
    public GameObject WatchVideoPanel;
    public GameObject GamePlayPanel;
    public GameObject DigitalLockPanel;
    public GameObject ClipBoardPanel;

    [Header("Subtitles")]
    public GameObject Title_1_Of_FindingBattery;

    public bool IsRewardEarnForHint = false;

    public GameObject QuestNumberImage;
    public Text QuestNumberText;

    public TimeHandler timeHandler;

    public bool IsWatchVideoSkip = false;

    private void Start()
    {
        //if(GameManager.instance.LevelSelected!=0)
        //{
        //    EnableHintButton(true);
        //}

        //if(GameManager.instance.LevelSelected==3)
        //{
        //    InvokeRepeating("GraduallyDeceaseHealthBar", 1f, 1.5f);
        //}

        QuestNumberText.text = (GameManager.instance.LevelSelected+1).ToString();
    }

    public void EnableUnEquipButton()
    {
        UnEquipButton.gameObject.SetActive(true);
    }


    public void DigitalLockEnable()
    {
        GamePlayPanel.SetActive(false);
        DigitalLockPanel.SetActive(true);
        DigitalLockPanel.transform.GetChild(0).DOScale(1, 0.4f).SetEase(Ease.InBounce).OnComplete(delegate
        {
            DigitalLockPanel.transform.GetChild(0).GetChild(0).DOScale(1, 0.4f).SetEase(Ease.InBounce).OnComplete(delegate
            {

            });
        });
    }

    public void DigitalLockDisable()
    {
        DigitalLockPanel.transform.GetChild(0).GetChild(0).DOScale(0, 0.6f).SetEase(Ease.OutBounce).OnComplete(delegate
        {
            DigitalLockPanel.transform.GetChild(0).DOScale(0, 0.6f).SetEase(Ease.OutBounce).OnComplete(delegate
            {
                DigitalLockPanel.SetActive(false);
                GamePlayPanel.SetActive(true);
                
            });

        });
    }

    public void ClipboardPanelEnable()
    {
        GamePlayPanel.SetActive(false);
        ClipBoardPanel.SetActive(true);
        ClipBoardPanel.transform.GetChild(0).DOScale(1, 0.4f).SetEase(Ease.InBounce).OnComplete(delegate
        {
            ClipBoardPanel.transform.GetChild(0).GetChild(0).DOScale(1, 0.4f).SetEase(Ease.InBounce).OnComplete(delegate
            {

            });
        });
    }

    public void ClipboardPanelDisable()
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.GenericButtonClick);
        ClipBoardPanel.transform.GetChild(0).GetChild(0).DOScale(0, 0.6f).SetEase(Ease.OutBounce).OnComplete(delegate
        {
            ClipBoardPanel.transform.GetChild(0).DOScale(0, 0.6f).SetEase(Ease.OutBounce).OnComplete(delegate
            {
                ClipBoardPanel.SetActive(false);
                GamePlayPanel.SetActive(true);
            });

        });
    }

    public void EnableInventory()
    {
        if (InventoryPanel.activeSelf)
        {
            InventoryPanel.SetActive(false);
        }
        else
        {
            InventoryPanel.SetActive(true);
        }
        
    }

    public void EnableHintButton(bool val)
    {
        HintButton.gameObject.SetActive(val);
    }

    public void DisableUnEquipButton()
    {
        UnEquipButton.gameObject.SetActive(false);
    }

    public void OnAdButtonClick()
    {
        timeHandler.Chk = false;
        MonetizationManager.instance.ShowRewardedVideoMediation();
    }
    public void EnableWinPanel(bool val)
    {
        QuestNumberImage.SetActive(true);
        //MonetizationManager.instance.ShowInterstitialMediation();
        SoundManager.instance.StopBackgroundMusic(AudioClipsSource.Instance.MainMenuClip);
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.LevelSuccessClip);
        GamePlayHandler.Instance.Enemy.SetActive(false);
        
        PausePanel.SetActive(false);
        if (GameManager.instance.SelectMode == 0)
        {
            
            
            
            if (GameManager.instance.LevelSelected == 4)
            {
                GameManager.instance.FirstModeCompleteCheck = true;
            }
            else
            {
                GamePlayHandler.Instance.WinLevel(GameManager.instance.LevelSelected);
            }
        }
        else
        {
            GamePlayHandler.Instance.WinLevel(GameManager.instance.LevelSelected);
            if (GameManager.instance.LevelSelected == 12)
            {
                WinLevelPanel.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                NewChapter.gameObject.SetActive(true);
            }
        }
        

        WinLevelPanel.gameObject.SetActive(true);
        WinLevelPanel.transform.GetChild(0).DOScale(1, 0.4f).SetEase(Ease.InSine).OnComplete(delegate
        {
            WinLevelPanel.transform.GetChild(0).transform.GetChild(0).DOScale(1, 0.4f).SetEase(Ease.InBounce).OnPlay(delegate
            {
                WinLevelPanel.transform.GetChild(0).transform.GetChild(1).DOScale(1, 0.4f).SetEase(Ease.InBounce).OnComplete(delegate
                {
                    WinLevelPanel.transform.GetChild(0).transform.GetChild(2).DOScale(1, 0.4f).SetEase(Ease.InBounce).OnComplete(delegate
                    {
                        WinLevelPanel.transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).DOScale(1, 0.3f).SetEase(Ease.InBounce).OnComplete(delegate
                        {
                            Time.timeScale = 0.0001f;
                        });

                    });
                });
            });

        });
        GameManager.instance.SelectMode = GamePlayHandler.Instance.playerPrefsHandler.GetGameMode();
    }

    public void ActivateWinPanel()
    {
        //Destroy(GamePlayHandler.Instance.AllLevels[GameManager.instance.LevelSelected].Level,1);
        EnableWinPanel(true);
    }
    public void EnableFailPanel(bool val)
    {
        //MonetizationManager.instance.ShowInterstitialMediation();
        DoctorCam.SetActive(false);
        GamePlayHandler.Instance.ClonedLevel.SetActive(false);
        EnableWatchVideoPanel();
    }

    public void ActivateFailPanel()
    {
        QuestNumberImage.SetActive(true);
        SoundManager.instance.StopBackgroundMusic(AudioClipsSource.Instance.MainMenuClip);
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.LevelFailedClip);
        if (GameManager.instance.LevelSelected == 2)
        {
            GamePlayHandler.Instance.ClonedLevel.SetActive(false);
        }
        GamePlayHandler.Instance.Enemy.SetActive(false);
        FailLevelPanel.SetActive(true);
        FailLevelPanel.transform.GetChild(0).DOScale(1, 0.3f).SetEase(Ease.InBounce).OnComplete(delegate
        {
            FailLevelPanel.transform.GetChild(0).GetChild(0).DOScale(1, 0.3f).SetEase(Ease.InBounce).OnComplete(delegate
            {
                FailLevelPanel.transform.GetChild(0).GetChild(1).DOScale(1, 0.3f).SetEase(Ease.InBounce).OnComplete(delegate
                {
                    Time.timeScale = 0.0001f;
                });
            });

        });
    }

    public void EnableHealthBar(bool val) {
        HealthBar.SetActive(val);
    }

    public void EnableObjectivePanel(bool val)
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.genericButtonClickBack);
        if (val)
        {
            QuestNumberImage.SetActive(true);
            ObjectivePanel.SetActive(val);
            ObjectivePanel.transform.GetChild(0).DOScale(1, 0.6f).SetEase(Ease.InBounce).OnComplete(delegate
            {
                ObjectivePanel.transform.GetChild(0).GetChild(0).DOScale(1, 0.6f).SetEase(Ease.InBounce).OnComplete(delegate
                {
                    
                    Time.timeScale = 0;
                });
            });
        }
        else
        {
            Time.timeScale = 1;
            
            ObjectivePanel.transform.GetChild(0).GetChild(0).DOScale(0, 0.6f).SetEase(Ease.OutBounce).OnComplete(delegate
            {
                ObjectivePanel.transform.GetChild(0).DOScale(0, 0.6f).SetEase(Ease.OutBounce).OnComplete(delegate
                {
                    QuestNumberImage.SetActive(false);
                    ObjectivePanel.SetActive(false);
                    GamePlayHandler.Instance.PlayerControllerUI.SetActive(true);
                    ObjectiveButton.gameObject.SetActive(true);
                });

            });
        }
        

     }

    public void EnableHintPanel(bool val)
    {
        if(IsRewardEarnForHint)
        {
            SoundManager.instance.PlayEffect(AudioClipsSource.Instance.genericButtonClickBack);
            if (val)
            {
                HintPanel.SetActive(val);
                HintPanel.transform.GetChild(0).DOScale(1, 0.6f).SetEase(Ease.InBounce).OnComplete(delegate
                {
                    HintPanel.transform.GetChild(0).GetChild(0).DOScale(1, 0.6f).SetEase(Ease.InBounce).OnComplete(delegate
                    {
                        Time.timeScale = 0;
                    });
                });
            }
            else
            {
                Time.timeScale = 1;
                HintPanel.transform.GetChild(0).GetChild(0).DOScale(0, 0.6f).SetEase(Ease.OutBounce).OnComplete(delegate
                {
                    HintPanel.transform.GetChild(0).DOScale(0, 0.6f).SetEase(Ease.OutBounce).OnComplete(delegate
                    {
                        HintPanel.SetActive(false);
                    });

                });
            }
        }
        else
        {
            //MonetizationManager.instance.ShowRewardedVideoMediation();
        }
        
    }

    public void SetHealthBar(int val)
    {
        GamePlayHandler.Instance.IsPlayerWin = true;
        FillImage.DOFillAmount(val, 0.4f).OnComplete(delegate
        {
            Invoke("ActivateWinPanel", 1);
        });
    }

    public void OnMainMenuButtonClick()
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.genericButtonClickBack);
        Time.timeScale = 1;
        GameManager.instance.ChangeScene(StaticVariables.MainMenu);
    }

    public void EnableWatchVideoPanel()
    {
        WatchVideoPanel.SetActive(true);
        timeHandler.gameObject.SetActive(true);
        timeHandler._timerStart = 10;
        timeHandler.ResetTimer();
        timeHandler.Chk = true;
        WatchVideoPanel.transform.GetChild(0).DOScale(1, 0.3f).SetEase(Ease.InBounce).OnComplete(delegate
        {
            WatchVideoPanel.transform.GetChild(0).transform.GetChild(0).DOScale(1, 0.3f).SetEase(Ease.InOutFlash).OnPlay(delegate
            {
                WatchVideoPanel.transform.GetChild(0).transform.GetChild(1).DOScale(1, 0.3f).SetEase(Ease.InOutFlash).OnComplete(delegate
                {
                     //Time.timeScale = 0.0001f;
                });
            });

        });
    }

    public void DisableWatchVideoPanel()
    {
        //Time.timeScale = 1;
        WatchVideoPanel.transform.GetChild(0).GetChild(1).DOScale(0, 0.3f).SetEase(Ease.OutBounce).OnComplete(delegate
        {
            WatchVideoPanel.transform.GetChild(0).GetChild(0).DOScale(0, 0.3f).SetEase(Ease.OutBounce).OnPlay(delegate
            {
                WatchVideoPanel.transform.GetChild(0).DOScale(0, 0.3f).SetEase(Ease.OutBounce).OnComplete(delegate
                {
                    WatchVideoPanel.SetActive(false);
                });
            });

        });
    }

    public void OnWatchVideoSkipButtonClick()
    {
        Time.timeScale = 1;
        IsWatchVideoSkip = true;
        WatchVideoPanel.transform.GetChild(0).GetChild(1).DOScale(0, 0.6f).SetEase(Ease.OutBounce).OnComplete(delegate
        {
            WatchVideoPanel.transform.GetChild(0).GetChild(0).DOScale(0, 0.6f).SetEase(Ease.OutBounce).OnComplete(delegate
            {
                WatchVideoPanel.transform.GetChild(0).DOScale(0, 0.6f).SetEase(Ease.OutBounce).OnComplete(delegate
                {
                    WatchVideoPanel.SetActive(false);
                    timeHandler.Chk = false;
                    ActivateFailPanel();
                });
            });

        });
    }

    public void Pause()
    {
        //MonetizationManager.instance.ShowInterstitialMediation();
        if (GameManager.instance.SelectMode != 0)
        {
            if (GameManager.instance.LevelSelected == 2)
            {
                GamePlayHandler.Instance.ClonedLevel.GetComponent<Level3Handler>().phoneSound.SetActive(false);
            }
        }

        QuestNumberImage.SetActive(true);
        SoundManager.instance.StopBackgroundMusic(AudioClipsSource.Instance.MainMenuClip);
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.genericButtonClickBack);
        PausePanel.SetActive(true);
        PausePanel.transform.GetChild(0).DOScale(1, 0.6f).SetEase(Ease.InBounce).OnComplete(delegate
        {
            PausePanel.transform.GetChild(0).transform.GetChild(0).DOScale(1, 0.3f).SetEase(Ease.InOutFlash).OnComplete(delegate
            {
                PausePanel.transform.GetChild(0).transform.GetChild(1).DOScale(1, 0.3f).SetEase(Ease.InOutFlash).OnComplete(delegate
                {
                    PausePanel.transform.GetChild(0).transform.GetChild(2).DOScale(1, 0.3f).SetEase(Ease.InOutFlash).OnComplete(delegate
                    {
                        
                        Time.timeScale = 0.0001f;

                    });
                });
            });

        });
    }

    public void Resume()
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.GenericButtonClick);
        Time.timeScale = 1;
        PausePanel.transform.GetChild(0).transform.GetChild(2).DOScale(0, 0.3f).SetEase(Ease.InOutFlash).OnComplete(delegate
        {

            PausePanel.transform.GetChild(0).transform.GetChild(1).DOScale(0, 0.3f).SetEase(Ease.InOutFlash).OnComplete(delegate
            {
                PausePanel.transform.GetChild(0).transform.GetChild(0).DOScale(0, 0.3f).SetEase(Ease.InOutFlash).OnComplete(delegate
                {
                    PausePanel.transform.GetChild(0).DOScale(0, 0.4f).SetEase(Ease.OutBounce).OnComplete(delegate
                    {
                        Time.timeScale = 1;
                        QuestNumberImage.SetActive(false);
                        PausePanel.SetActive(false);
                        SoundManager.instance.PlayBackgroundMusic(AudioClipsSource.Instance.MainMenuClip);
                        if (GameManager.instance.LevelSelected == 2)
                        {
                            GamePlayHandler.Instance.ClonedLevel.GetComponent<Level3Handler>().phoneSound.SetActive(true);
                        }

                    });


                });
            });

        });

    }

    public void RestartButton()
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.GenericButtonClick);
        Time.timeScale = 1f;
        if (GameManager.instance.SelectMode ==0)
        {
            GameManager.instance.ChangeScene(StaticVariables.FirstScene);
        }
        else
        {
            GameManager.instance.ChangeScene(StaticVariables.GamePlayMenu);
        }
        
    }

    public void OnClickObjectiveButton() {
        ObjectivePanel.SetActive(true);
        Time.timeScale = 0.0001f;

    }

    public void OnClickPauseButton() {
        PausePanel.SetActive(true);
        Time.timeScale = 0.0001f;
    }

    public IEnumerator EnableWinPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        EnableWinPanel(true);
    }

}
