using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActiveCutCam : MonoBehaviour
{
    public PlayableDirector timeline;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            StartCoroutine(Wait((float)timeline.duration));
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    IEnumerator Wait(float delay)
    {
        timeline.gameObject.SetActive(true);
        GamePlayHandler.Instance.PlayerControllerUI.SetActive(false);
        yield return new WaitForSeconds(delay);
        timeline.gameObject.SetActive(false);
        GamePlayHandler.Instance.PlayerControllerUI.SetActive(true);
    }
}
