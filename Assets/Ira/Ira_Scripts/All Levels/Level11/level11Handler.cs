using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level11Handler : MonoBehaviour
{
    public GenericLevel genericLevel;
    void Start()
    {
        //GamePlayHandler.Instance._pickItem = genericLevel.PickUpObjects[0];
        GamePlayHandler.Instance.InteractItem = genericLevel.InteractableObjects[0];
    }
}
