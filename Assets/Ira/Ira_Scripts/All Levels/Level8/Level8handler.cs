using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8handler : MonoBehaviour
{
    public GenericLevel genericLevel;
    public GameObject BrakingPartical;

    void Start()
    {
        GamePlayHandler.Instance.InteractItem = genericLevel.InteractableObjects[0];
        //GamePlayHandler.Instance.endCutCam = genericLevel.EndCutCam;
    }
}
