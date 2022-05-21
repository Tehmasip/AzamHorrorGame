using UnityEngine;
[CreateAssetMenu(fileName = "AdData", menuName = "Settings/AdData", order = 1)]
public class MonetizationData : ScriptableObject
{
    [Header("Priority Instertial")]
    public string FirstPriorityInterstitial= "Admob";
    public string SecondPriorityInterstitial= "Unity";

    [Header("Priority Rewarded")]
    public string FirstPriorityRewarded="Admob";
    public string SecondPriorityRewarded="Unity";

    [Header("Priority Banner")]
    public string FirstPriorityBanner = "Admob";
    public string SecondPriorityBanner = "Unity";
}