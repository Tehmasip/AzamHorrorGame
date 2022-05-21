using System;
using UnityEngine;
using UnityEngine.UI;

public class MonetizationManager : MonoBehaviour
{
    #region Singleton
    public static MonetizationManager instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    int countInterstitialPriority = 0;
    int countRewardedPriority = 0;
    int countBannerPrioity;

    string Interstitialpriorirty;
    string Rewardedpriorirty;
    string BannerPriority;

    public MonetizationData Gdata;

    #region Unity Variables
    [Header("Unity Id")]
    public string UnityId;
    #endregion

    #region Admob Variables
    [Header("Admob Ids")]
    public string AdmobAppId;
    public string AdmobInterstitialId;
    public string AdmobRewardedId;
    public string AdmobBannerId;
    #endregion

    #region Banner Position
    [Header("Banner")]
    public BannerAdPos BannerAdPosition;
    public enum BannerAdPos
    {
        BOTTOM,
        TOP
    }
    #endregion

    void Start()
    {
        this.gameObject.AddComponent<AdmobMonetization>();
        this.gameObject.AddComponent<UnityMonetization>();

        GetComponent<AdmobMonetization>().AppID = AdmobAppId;
        GetComponent<AdmobMonetization>().InterstitialID = AdmobInterstitialId;
        GetComponent<AdmobMonetization>().RewardedID = AdmobRewardedId;
        GetComponent<AdmobMonetization>().BannerID = AdmobBannerId;

        GetComponent<UnityMonetization>().UnityAdID = UnityId;

        BannerPosition();
        
    }

    public void LoadAllAds()
    {
        LoadRewardedVideoMediation();
    }

    #region Banner
    void BannerPosition()
    {
        if (BannerAdPosition == BannerAdPos.BOTTOM)
        {
            GetComponent<AdmobMonetization>().BannerAdPosition = AdmobMonetization.BannerAdPos.BOTTOM;
        }
        else
        {
            GetComponent<AdmobMonetization>().BannerAdPosition = AdmobMonetization.BannerAdPos.TOP;
        }
    }

    public void LoadBanner()
    {
        //AdMob
        GetComponent<AdmobMonetization>().AssignBannerPoitions();
    }

    public void ShowBanner()
    {
        if(PlayerPrefs.GetInt("RemoveAds") !=2)
        {
            LoadBanner();
            PriorityBanner();
            
        }
       
    }

    public void PriorityBanner()
    {
        countBannerPrioity++;

        if (countBannerPrioity == 1)
        {
            BannerPriority = Gdata.FirstPriorityBanner;
            SwitchBanner();
        }
        else if (countBannerPrioity == 2)
        {
            BannerPriority = Gdata.SecondPriorityBanner;
            SwitchBanner();
        }

    }
    void SwitchBanner()
    {
        if (BannerPriority != null)
        {
            switch (BannerPriority)
            {
                case "Admob":
                    countBannerPrioity = 0;
                    GetComponent<AdmobMonetization>().ShowBannerAd();
                    break;
                case "Unity":
                    if (GetComponent<UnityMonetization>().IsUnityBannerAdLoaded())
                    {
                        countBannerPrioity = 0;
                        GetComponent<UnityMonetization>().ShowUnityBannerAd();
                    }
                    else
                    {
                        PriorityBanner();
                    }
                    break;
            }
        }
    }

    public void DestroyBanner()
    {
        if(BannerPriority=="Admob")
        {
            GetComponent<AdmobMonetization>().DestroyBannerAdmob();
        }
        else if(BannerPriority=="Unity")
        {
            GetComponent<UnityMonetization>().DestroyBannerAd();
        }
        
    }
    #endregion

    #region Interstitial
    public void LoadInterstitialMediation()
    {
        //Unity
        //AutoLoad

        //Admob
        GetComponent<AdmobMonetization>().RequestInterstitial();
    }
    public void ShowInterstitialMediation()
    {
        if(PlayerPrefs.GetInt("RemoveAds") !=2)
        {
            PriorityInterstitial();
        }
        
    }
    void PriorityInterstitial()
    {
        countInterstitialPriority++;

        if (countInterstitialPriority == 1)
        {
            Interstitialpriorirty = Gdata.FirstPriorityInterstitial;
            SwitchInterstitial();
        }
        else if (countInterstitialPriority == 2)
        {
            Interstitialpriorirty = Gdata.SecondPriorityInterstitial;
            SwitchInterstitial();
        }

    }
    void SwitchInterstitial()
    {
        if (Interstitialpriorirty != null)
        {
            switch (Interstitialpriorirty)
            {
                case "Admob":
                    if (GetComponent<AdmobMonetization>().IsInterstitialLoaded())
                    {
                        countInterstitialPriority = 0;
                        GetComponent<AdmobMonetization>().ShowInterstitialAd();
                    }
                    else
                    {
                        PriorityInterstitial();
                    }
                    break;
                case "Unity":
                    if (GetComponent<UnityMonetization>().IsUnityInterstitialLoaded())
                    {
                        countInterstitialPriority = 0;
                        GetComponent<UnityMonetization>().ShowUnityInterstitalAd();
                    }
                    else
                    {
                        PriorityInterstitial();
                    }
                    break;
            }
        }
    }
    #endregion

    #region Rewarded
    public void LoadRewardedVideoMediation()
    {
        //Admob
        GetComponent<AdmobMonetization>().RequestRewardedVideo();

        //Unity
        //Auto Loaded

    }
    public void ShowRewardedVideoMediation()
    {
        PriorityRewarded();
    }
    void PriorityRewarded()
    {
        countRewardedPriority++;

        if (countRewardedPriority == 1)
        {
            Rewardedpriorirty = Gdata.FirstPriorityRewarded;
            SwitchRewarded();
        }
        else if (countRewardedPriority == 2)
        {
            Rewardedpriorirty = Gdata.SecondPriorityRewarded;
            SwitchRewarded();
        }

    }
    void SwitchRewarded()
    {
        if (Rewardedpriorirty != null)
        {
            switch (Rewardedpriorirty)
            {
                case "Admob":
                    if (GetComponent<AdmobMonetization>().IsAdMobRewardedAdLoaded())
                    {
                        countRewardedPriority = 0;
                        GetComponent<AdmobMonetization>().ShowRewardedVideo();
                    }
                    else
                    {
                        PriorityRewarded();
                    }
                    break;
                case "Unity":
                    if (GetComponent<UnityMonetization>().IsUnityRewardedAdIsReady())
                    {
                        countRewardedPriority = 0;
                        GetComponent<UnityMonetization>().ShowUnityRewardedVideoAd();
                    }
                    else
                    {
                        PriorityRewarded();
                    }
                    break;
            }
        }
    }

    public void RewardedVideoComplete()
    {
        GamePlayHandler.Instance.RevivePlayer();
    }
    #endregion
}
