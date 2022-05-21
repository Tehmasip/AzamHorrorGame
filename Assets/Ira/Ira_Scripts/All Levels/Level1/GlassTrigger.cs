using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GlassTrigger : MonoBehaviour
{
    public GenericLevel genericLevel;
    public GameObject FloorMarker;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player"&& this.GetComponent<BoxCollider>().enabled)
        {
            this.GetComponent<BoxCollider>().enabled = false;
            if(FloorMarker != null)
            {
                FloorMarker.SetActive(false);
            }
            GamePlayHandler.Instance.PlayerControllerUI.SetActive(false);
            if (genericLevel != null)
            {
                GamePlayHandler.Instance.EnableEndCutCam(genericLevel.EndCutCam);
            }
        }
    }
}
