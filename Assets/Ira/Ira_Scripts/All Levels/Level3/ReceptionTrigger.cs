using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionTrigger : MonoBehaviour
{
    public GenericLevel genericLevel;
    public GameObject phonering;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && this.GetComponent<BoxCollider>().enabled)
        {
            phonering.GetComponent<AudioSource>().Stop();
            this.GetComponent<BoxCollider>().enabled = false;
            GamePlayHandler.Instance.PlayerControllerUI.SetActive(false);
            UIManager.Instance.ActivateWinPanel();
            //GamePlayHandler.Instance.EnableEndCutCam(genericLevel.EndCutCam);
        }
    }
}
