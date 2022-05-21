using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level2Handler : MonoBehaviour
{
    public int pickItemCount=0;
    public GameObject ContinuousBreathing;

    private void Start()
    {
        GamePlayHandler.Instance.ContinuousBreathing = ContinuousBreathing;
    }
}
