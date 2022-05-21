using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericLevel : MonoBehaviour
{
    [Header("Enemy Positions")]
    public Transform EnemyStartPos;
    public Transform[] EnemyAIPos;

    [Header("Player Positions")]
    public Transform PlayerPos;
    [Header("Player Friends")]
    public GameObject[] Friends;

    [Header("Objective Text")]
    public Text StartObjectiveText;

    [Header("Hint Text")]
    public Text HintText;


    [Header("CutScenes")]
    public GameObject StartCutCam;
    public GameObject EndCutCam;

    [Header("Pickup Object")]
    public GameObject[] PickUpObjects;

    [Header("Interactable Object")]
    public GameObject[] InteractableObjects;

    public GameObject FloorMarker;
    public GameObject PathDirection;

    private void Start()
    {
        GamePlayHandler.Instance.AllLevels[GameManager.instance.LevelSelected].EnemyStartPos = EnemyStartPos;
        GamePlayHandler.Instance.AllLevels[GameManager.instance.LevelSelected].EnemyAIPos = EnemyAIPos;
        GamePlayHandler.Instance.AllLevels[GameManager.instance.LevelSelected].PlayerPos = PlayerPos;
        GamePlayHandler.Instance.AssignEnemyPos();
        GamePlayHandler.Instance.EnableStartCutCam(StartCutCam);
        if(EndCutCam!=null)
        {
            GamePlayHandler.Instance.endCutCam = EndCutCam;
        }
    }
}
