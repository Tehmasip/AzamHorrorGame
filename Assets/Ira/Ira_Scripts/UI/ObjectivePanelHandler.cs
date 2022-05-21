using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePanelHandler : MonoBehaviour
{
    public GameObject objectiveText;
    private void Start()
    {
        objectiveText.transform.GetChild(GameManager.instance.LevelSelected).gameObject.SetActive(true);
        GamePlayHandler.Instance.PlayerControllerUI.SetActive(false);
        UIManager.Instance.ObjectiveButton.gameObject.SetActive(false);
    }
    public void OnClickPlayNow()
    {
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.GenericButtonClick);
        Time.timeScale = 1f;
        UIManager.Instance.ObjectivePanel.SetActive(false);
        GamePlayHandler.Instance.PlayerControllerUI.SetActive(true);
        UIManager.Instance.ObjectiveButton.gameObject.SetActive(true);
    }
}
