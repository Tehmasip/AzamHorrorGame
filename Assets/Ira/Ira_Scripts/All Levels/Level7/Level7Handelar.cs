using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level7Handelar : MonoBehaviour
{
    public GenericLevel genericLevel;
    void Start()
    {
        GamePlayHandler.Instance._pickItem = genericLevel.PickUpObjects[0];
    }

}
