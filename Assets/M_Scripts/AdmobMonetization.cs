using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
public class AdmobMonetization : MonoBehaviour
{
    [Header("Google App ID")]
    public string AppID;

    [Header("Ad ID")]

    public string BannerID;
    public string InterstitialID;
    public string RewardedID;

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardBasedVideo;

    bool IsBannerLoaded=false;
    public enum BannerAdPos
    {
        BOTTOM,
        TOP
    };

    public BannerAdPos BannerAdPosition;
    private AdPosition adPosition = AdPosition.Bottom;

    // Start is called before the first frame update
    void Start()
    {
        //For Admob
        
        //MobileAds.Initialize(AppID);
        
        MobileAds.Initialize(initStatus => { });
    }


    #region Banner
    public void AssignBannerPoitions()
    {
        switch (BannerAdPosition)
        {
            case BannerAdPos.BOTTOM:
                adPosition = AdPosition.Bottom;
                break;
            case BannerAdPos.TOP:
                adPosition = AdPosition.Top;
                break;
        }
    }
    public void ShowBannerAd()
    {

#if UNITY_ANDROID
        string adUnitId = BannerID;
#elif UNITY_IPHONE
            string adUnitId = BannerID;
#else
            string adUnitId = BannerID;
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, adPosition);

        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnBannerAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnBannerAdFailedToLoaded;

        RequestBanner();

    }

    public void RequestBanner()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }
   
    public void DestroyBannerAdmob()
    {
        this.bannerView.Hide();
        this.bannerView.Destroy();
    }

    #region CallBacks
    public void HandleOnBannerAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Bannera Ad Loaded");
    }
    public void HandleOnBannerAdFailedToLoaded(object sender, EventArgs args)
    {
        MonetizationManager.instance.PriorityBanner();
        Debug.Log("Bannera Ad failed to Loaded");
    }
    #endregion

    #endregion

    #region Interstitial
    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = InterstitialID;
#elif UNITY_IPHONE
        string adUnitId = InterstitialID;
#else
        string adUnitId = "unexpected_platform";
#endif
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnInterstitialAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnInterstitialAdFailedToLoaded;
        this.interstitial.OnAdClosed += HandleOnInterstitialAdClosed;


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }
    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    public bool IsInterstitialLoaded()
    {
        if (this.interstitial.IsLoaded())
        {
            return true;
        }
        else
        {
            RequestInterstitial();
            return false;
        }
    }

    #region CallBacks
    public void HandleOnInterstitialAdLoaded(object sender, EventArgs args)
    {

    }
    public void HandleOnInterstitialAdFailedToLoaded(object sender, EventArgs args)
    {

    }
    public void HandleOnInterstitialAdClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
    }
    #endregion

    #endregion

    #region Rewarded Video

    public void RequestRewardedVideo()
    {
        //rewardBasedVideo = RewardBasedVideoAd.Instance;
        
        this.rewardBasedVideo = new RewardedAd(RewardedID);
        
        // Called when an ad request has successfully loaded.
        this.rewardBasedVideo.OnAdLoaded += HandleOnRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardBasedVideo.OnAdFailedToLoad += HandleOnRewardedAdFailed;
        // Called when an ad completed.
        this.rewardBasedVideo.OnUserEarnedReward += HandleOnRewardedVideoComplete;
        // Called when an ad open.
        this.rewardBasedVideo.OnAdOpening += HandleOnRewardedAdOpen;
        // Called when an ad open.
        this.rewardBasedVideo.OnAdClosed += HandleOnRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardBasedVideo.LoadAd(request);
    }

    public void ShowRewardedVideo()
    {
        if (this.rewardBasedVideo.IsLoaded())
        {
            this.rewardBasedVideo.Show();
        }
    }

    public bool IsAdMobRewardedAdLoaded()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #region Call Backs
    //Call Backs
    public void HandleOnRewardedAdLoaded(object sender, EventArgs args)
    {

    }
    public void HandleOnRewardedAdOpen(object sender, EventArgs args)
    {

    }
    public void HandleOnRewardedAdFailed(object sender, EventArgs args)
    {

    }
    public void HandleOnRewardedAdClosed(object sender, EventArgs args)
    {
        RequestRewardedVideo();
    }

    public void HandleOnRewardedVideoComplete(object sender, EventArgs args)
    {
        MonetizationManager.instance.RewardedVideoComplete();
    }
    #endregion

    #endregion

}
