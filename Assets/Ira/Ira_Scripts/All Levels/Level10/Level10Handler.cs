using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level10Handler : MonoBehaviour
{
    public GenericLevel genericLevel;
    public GameObject FloorMarker;
    void Start()
    {
        GamePlayHandler.Instance._pickItem = genericLevel.PickUpObjects[0];
        //GamePlayHandler.Instance.InteractItem = genericLevel.InteractableObjects[0];
    }
}
