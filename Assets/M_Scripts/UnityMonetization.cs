using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using System;

public class UnityMonetization : MonoBehaviour, IUnityAdsListener
{
    public string UnityAdID;

    bool TestMode = false;

    string InterstitialAd = "Interstitial_Android";
    string BannerAd = "Banner_Android";
    string RewardedAd = "Rewarded_Android";

    // Start is called before the first frame update
    void Start()
    {
        //For Unity Ads
        Advertisement.AddListener(this);
        Advertisement.Initialize(UnityAdID, TestMode);
    }

    #region Banner
    public void ShowUnityBannerAd()
    {
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(BannerAd);
    }

    public bool IsUnityBannerAdLoaded()
    {
        if (Advertisement.IsReady(BannerAd))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DestroyBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    #endregion
    #region Interstitial
    public void ShowUnityInterstitalAd()
    {
        Advertisement.Show(InterstitialAd);
    }
    public bool IsUnityInterstitialLoaded()
    {
        if (Advertisement.IsReady(InterstitialAd))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Rewarded
    public void ShowUnityRewardedVideoAd()
    {
        Advertisement.Show(RewardedAd);
    }

    public bool IsUnityRewardedAdIsReady()
    {
        if (Advertisement.IsReady(RewardedAd))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region CallBacks
    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            if (placementId == RewardedAd)
            {
                // Reward the user for watching the ad to completion.
                MonetizationManager.instance.RewardedVideoComplete();
            }
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == RewardedAd)
        {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }
    #endregion
}
