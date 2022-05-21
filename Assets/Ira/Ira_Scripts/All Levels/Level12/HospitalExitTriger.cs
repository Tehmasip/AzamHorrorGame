using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalExitTriger : MonoBehaviour
{
    public GenericLevel genericLevel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && this.GetComponent<BoxCollider>().enabled)
        {
            this.GetComponent<BoxCollider>().enabled = false;
            //GamePlayHandler.Instance.PlayerControllerUI.SetActive(false);
            if(GameManager.instance.LevelSelected==12)
            {
                UIManager.Instance.KeyImage.gameObject.SetActive(false);
                StartCoroutine(EndCamAfterFewDelay());
            }
            else
            {
                UIManager.Instance.ActivateWinPanel();
            }
           
        }
    }

    public IEnumerator EndCamAfterFewDelay()
    {
        yield return new WaitForSeconds(1);
        GamePlayHandler.Instance.EnableEndCutCam(genericLevel.EndCutCam);
    }
}
