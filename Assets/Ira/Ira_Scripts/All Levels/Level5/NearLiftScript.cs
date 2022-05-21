using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearLiftScript : MonoBehaviour
{
    public GenericLevel genericLevel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && this.GetComponent<BoxCollider>().enabled)
        {
            this.GetComponent<BoxCollider>().enabled = false;
            GamePlayHandler.Instance.PlayerControllerUI.SetActive(false);
            //UIManager.Instance.ActivateWinPanel();
            GamePlayHandler.Instance.EnableEndCutCam(genericLevel.EndCutCam);
        }
    }
}
