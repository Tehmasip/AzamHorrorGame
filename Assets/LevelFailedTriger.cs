using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailedTriger : MonoBehaviour
{
    bool check;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && check ==false)
        {
            UIManager.Instance.EnableFailPanel(true);
            check = true;
        }
    }
}
