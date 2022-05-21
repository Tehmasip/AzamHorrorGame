using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class GamePlayHandler : MonoBehaviour
{
    //Singleton of GamePlay Class
    #region Singleton
    public static GamePlayHandler Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    [Header("All Levels of GamePlay")]
    public Levels[] AllLevels;

    [Header("Player of GamePlay")]
    public GameObject Player;
    public GameObject PlayerTorch;

    [Header("Enemy")]
    public GameObject Enemy;

    [Header("Layers")]
    public LayerMask Layer;


    public GameObject Canvas;
    public GameObject PlayerControllerUI;


    public bool PlayerDie = false;
    public bool IsPlayerWin = false;
    public bool IsSkip = false;

    public PlayableDirector PlayerFaintAnimation;

    public GameObject _pickItem;
    public GameObject InteractItem;

    public GameObject endCutCam;
    public GameObject StartCutCam;

    public PlayerPrefsHandler playerPrefsHandler;

    [Header("Others")]
    public GameObject Lift;//used in level 6
    public GameObject Hammor;//used in level 8
    public GameObject GlassWindow;//used in level 8
    public GameObject DoctorCubbord;//used in level 9
    public GameObject CubbordDoor;//used in level 10
    public GameObject ContinuousBreathing;
    public GameObject[] allPickupObjects;



    RaycastHit hit;

    public GameObject ClonedLevel;

    public Transform PlayerRevivePos;

    public GameObject Elevator;

    public physicWalk_MouseLook senstivity1;
    public physicWalk_MouseLook senstivity2;
    private void Start()
    {
        ActivateLevel();

        playerPrefsHandler.SetDay(playerPrefsHandler.GetDay() + 1);

        senstivity1.sensitivity += playerPrefsHandler.GetSenstivity();
        senstivity2.sensitivity += playerPrefsHandler.GetSenstivity();

        if (GameManager.instance.LevelSelected==12)
        {
            Elevator.SetActive(false);
        }
        //GamePlayHandler.Instance.playerPrefsHandler.SetLevelReached(GameManager.instance.LevelSelected);

        //SoundManager.instance.PlayBackgroundMusic(AudioClipsSource.Instance.MainMenuClip);
        //SoundManager.instance.MusicVolume = 0.5f;

        if (GameManager.instance.LevelSelected >= 5 && GameManager.instance.LevelSelected <= 10)
        {
            Lift.GetComponent<Animator>().SetBool(StaticVariables.LiftClose, true);
        }

        if (GameManager.instance.LevelSelected == 7) {
            Hammor.gameObject.SetActive(true);
        }
        if (GameManager.instance.LevelSelected == 8)
        {
            DoctorCubbord= ClonedLevel.GetComponent<Level9Handler>().Cupboard;
            DoctorCubbord.layer = LayerMask.NameToLayer("Interactable");
            InteractItem = DoctorCubbord;
        }
        if (GameManager.instance.LevelSelected==9) {
            CubbordDoor.layer = LayerMask.NameToLayer("Interactable");
            InteractItem = CubbordDoor;
        }
    }

    private void Update()
    {
        CrossHair();
    }
    public void ActivatePlayer()
    {
        Player.SetActive(false);
        Player.transform.localPosition = AllLevels[GameManager.instance.LevelSelected].PlayerPos.localPosition;
        Player.transform.localEulerAngles = AllLevels[GameManager.instance.LevelSelected].PlayerPos.localEulerAngles;
        Player.SetActive(true);
    }
    public void ActivateEnemy(bool val)
    {
        Enemy.SetActive(val);
    }

    void CrossHair()
    { 
        if (UIManager.Instance.CrossHairImage.gameObject.activeSelf)
        {
            if (Physics.Raycast(Player.GetComponentInChildren<Camera>().transform.position, Player.GetComponentInChildren<Camera>().transform.forward, out hit, 2, Layer))
            {
                switch (hit.collider.gameObject.tag)
                {
                    case "Pickups":
                        UIManager.Instance.PickButton.gameObject.SetActive(true);
                        UIManager.Instance.CrossHairImage.color = Color.yellow;
                        _pickItem = hit.transform.gameObject;
                        break;
                    case "Interactable":
                        UIManager.Instance.InteractableButton.gameObject.SetActive(true);
                        UIManager.Instance.CrossHairImage.color = Color.red;
                        InteractItem = hit.transform.gameObject;
                        break;
                    case "Enemy":
                        EnemyController.Instance.IsTowardsPlayer = true;
                        break;

                }

            }
            else
            {
                UIManager.Instance.PickButton.gameObject.SetActive(false);
                UIManager.Instance.InteractableButton.gameObject.SetActive(false);
                UIManager.Instance.CrossHairImage.color = Color.white;
                if (EnemyController.Instance != null)
                {
                    EnemyController.Instance.IsTowardsPlayer = false;
                }

            }
        }
    }

    #region All CutCams
    public void EnableStartCutCam(GameObject CutCam)
    {
        StartCutCam = CutCam;
        UIManager.Instance.SkipButton.gameObject.SetActive(true);
        UIManager.Instance.CinematicBars.SetActive(true);
        PlayerControllerUI.SetActive(false);
        CutCam.SetActive(true);
        CutCam.GetComponent<PlayableDirector>().Play();
        StartCoroutine(OnStartCutCamEnd((float)CutCam.GetComponent<PlayableDirector>().duration,CutCam));

    }

    public IEnumerator OnStartCutCamEnd(float duration,GameObject startCutCam)
    {
        yield return new WaitForSeconds(duration);
        OnSkipButtonClick();
    }

    public void OnSkipButtonClick()
    {
        if (GameManager.instance.SelectMode ==0)
        {
            if (!IsSkip)
            {
                UIManager.Instance.CinematicBars.SetActive(false);
                if (ClonedLevel.GetComponent<GenericLevel>().PathDirection != null)
                {
                    ClonedLevel.GetComponent<GenericLevel>().PathDirection.SetActive(true);
                }
                
                IsSkip = true;
                UIManager.Instance.SkipButton.gameObject.SetActive(false);
                StartCutCam.SetActive(false);
                ActivatePlayer();
                PlayerControllerUI.SetActive(true);
                ActivateEnemy(true);
                if (ClonedLevel.GetComponent<GenericLevel>().Friends.Length > 0)
                {
                    for(int i=0;i< ClonedLevel.GetComponent<GenericLevel>().Friends.Length; i++)
                    {
                        if (ClonedLevel.GetComponent<GenericLevel>().Friends[i] != null)
                        {
                            ClonedLevel.GetComponent<GenericLevel>().Friends[i].SetActive(true);
                        }
                    }
                }
            }
        }
        else
        {
            if (!IsSkip)
            {
                if (ClonedLevel.GetComponent<GenericLevel>().PathDirection != null)
                {
                    ClonedLevel.GetComponent<GenericLevel>().PathDirection.SetActive(true);
                }
                UIManager.Instance.CinematicBars.SetActive(false);
                ClonedLevel.GetComponent<GenericLevel>().FloorMarker.SetActive(true);
                IsSkip = true;
                UIManager.Instance.SkipButton.gameObject.SetActive(false);
                StartCutCam.SetActive(false);
                ActivatePlayer();
                PlayerControllerUI.SetActive(true);

                if (GameManager.instance.LevelSelected != 12)
                {
                    ActivateEnemy(true);
                }

                if (GameManager.instance.LevelSelected >= 10 && GameManager.instance.LevelSelected <= 12)
                {
                    UIManager.Instance.KeyImage.gameObject.SetActive(true);
                }

                switch (GameManager.instance.LevelSelected)
                {
                    case 1:
                        ContinuousBreathing.SetActive(true);
                        break;
                    case 8:
                        Enemy.GetComponent<EnemyAI>()._currentState = EnemyAI.EnemyStates.Chasing;
                        break;
                    case 9:
                        ClonedLevel.GetComponent<Level10Handler>().FloorMarker.SetActive(true);
                        break;
                    case 12:
                        Elevator.transform.position = new Vector3(Elevator.transform.position.x, -17.61f, Elevator.transform.position.z);
                        Elevator.SetActive(true);
                        break;
                }
            }
        }
        
    }

    public void EnableEndCutCam(GameObject CutCam)
    {
        if (ClonedLevel.GetComponent<GenericLevel>().Friends.Length > 0)
        {
            for (int i = 0; i < ClonedLevel.GetComponent<GenericLevel>().Friends.Length; i++)
            {
                if (ClonedLevel.GetComponent<GenericLevel>().Friends[i] != null)
                {
                    ClonedLevel.GetComponent<GenericLevel>().Friends[i].SetActive(false);
                }
            }
        }
        if (ClonedLevel.GetComponent<GenericLevel>().PathDirection != null)
        {
            ClonedLevel.GetComponent<GenericLevel>().PathDirection.SetActive(false);
        }
        
        PlayerControllerUI.SetActive(false);
        CutCam.SetActive(true);
        Player.SetActive(false);
        Enemy.SetActive(false);
        
        //CutCam.SetActive(true);
        CutCam.GetComponent<PlayableDirector>().Play();
        StartCoroutine(OnEndCutCamEnd((float)CutCam.GetComponent<PlayableDirector>().duration,CutCam));

    }

    public IEnumerator OnEndCutCamEnd(float duration, GameObject endCutCam)
    {
        yield return new WaitForSeconds(duration);
        ActivatePlayer();
        endCutCam.SetActive(false);
        PlayerControllerUI.SetActive(false);
        UIManager.Instance.ActivateWinPanel();

    }
    #endregion


    #region Interact and Pick
    public void OnInteractButtonClick()
    {
        //Change the layer of Interactable Object from Interactable Layer to Default layer
        InteractItem.layer = LayerMask.NameToLayer("Default");
        UIManager.Instance.InteractableButton.gameObject.SetActive(false);

        switch (GameManager.instance.LevelSelected)
        {
            case 1:
                allPickupObjects = AllLevels[GameManager.instance.LevelSelected].Level.GetComponent<GenericLevel>().PickUpObjects;
                for (int i = 0; i < allPickupObjects.Length; i++)
                {
                    allPickupObjects[i].layer= LayerMask.NameToLayer("PickUps");
                }
                //InteractItem.GetComponent<Animator>().SetBool("OpenDoor", true);
                InteractItem.GetComponent<Animator>().Play("MedicineDoorOpen");
                SoundManager.instance.PlayEffect(AudioClipsSource.Instance.DoorOpen);
                break;
            case 7:
                
                EnableEndCutCam(endCutCam);
                //UIManager.Instance.ActivateWinPanel();
                
                break;
            case 8:
                ClonedLevel.GetComponent<Level9Handler>().FloorMarker.SetActive(false);
                EnableEndCutCam(endCutCam);
                //UIManager.Instance.ActivateWinPanel();
                break;
            case 9:
                _pickItem.layer = LayerMask.NameToLayer("PickUps");
                InteractItem.GetComponent<Animator>().SetBool(StaticVariables.CubbordOpen, true);
                SoundManager.instance.PlayEffect(AudioClipsSource.Instance.DoorOpen);
                break;
            case 10:
                //add ending cutcam
                Enemy.SetActive(false);
                UIManager.Instance.ActivateWinPanel();
                break;
        }
    }

    public void OnPickUpButtonClick()
    {
        //Change the layer of Interactable Object from Interactable Layer to Default layer
        _pickItem.layer = LayerMask.NameToLayer("Default");
        UIManager.Instance.PickButton.gameObject.SetActive(false);
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.PickUpSound);

        switch (GameManager.instance.LevelSelected)
        {
            case 1:
                ClonedLevel.GetComponent<Level2Handler>().pickItemCount++;
                Destroy(_pickItem);
                if(ClonedLevel.GetComponent<Level2Handler>().pickItemCount== 2)
                {
                    ContinuousBreathing.SetActive(false);
                    UIManager.Instance.ActivateWinPanel();
                }
                break;
            case 6:
                Destroy(_pickItem);
                UIManager.Instance.ActivateWinPanel();
                break;
            case 9:
                Destroy(_pickItem);
                UIManager.Instance.ActivateWinPanel();
                break;
        }
    }

    #endregion


    public void RevivePlayer()
    {
        Enemy.SetActive(false);
        AssignEnemyPos();
        Enemy.SetActive(true);

        PlayerFaintAnimation.gameObject.SetActive(false);

        //Player.SetActive(false);
        //Player.transform.localPosition = PlayerRevivePos.localPosition;
        //Player.transform.localEulerAngles = PlayerRevivePos.localEulerAngles;

        ClonedLevel.SetActive(true);
        UIManager.Instance.PlayerfaintEffect.SetActive(false);
        Enemy.GetComponent<EnemyAI>()._currentWaypoint = 0;

        Player.SetActive(true);
        PlayerControllerUI.SetActive(true);
        PlayerDie = false;

        UIManager.Instance.DisableWatchVideoPanel();
    }

    public void AssignEnemyPos()
    {
        if (AllLevels[GameManager.instance.LevelSelected].EnemyStartPos != null)
        {
            Vector3 currentEnemyPos = AllLevels[GameManager.instance.LevelSelected].EnemyStartPos.position;
            Vector3 currentEnemyRotation = AllLevels[GameManager.instance.LevelSelected].EnemyStartPos.eulerAngles;
            Enemy.transform.position = new Vector3(currentEnemyPos.x, currentEnemyPos.y, currentEnemyPos.z);
            Enemy.transform.eulerAngles = new Vector3(currentEnemyRotation.x, currentEnemyRotation.y, currentEnemyRotation.z);
        }

    }
    public void ActivateLevel()
    {
        ClonedLevel = null;
        ClonedLevel= Instantiate(AllLevels[GameManager.instance.LevelSelected].Level);
    }

    public void WinLevel(int l)
    {
        //if (l == playerPrefsHandler.GetLevelReached())
        //{
        //    playerPrefsHandler.SetLevelReached(playerPrefsHandler.GetLevelReached() + 1);
        //    GameManager.instance.LevelSelected = playerPrefsHandler.GetLevelReached();
        //}
        if (GameManager.instance.SelectMode ==0)
        {
            
            if (playerPrefsHandler.GetLevelReachedNewMode() <= l)
            {
                playerPrefsHandler.SetLevelReachedNewMode(playerPrefsHandler.GetLevelReachedNewMode() + 1);

            }
            
        }
        else
        {
            if (playerPrefsHandler.GetLevelReached() <= l)
            {

                playerPrefsHandler.SetLevelReached(playerPrefsHandler.GetLevelReached() + 1);
            }
        }
        
        
    }

    public IEnumerator HitWindow(float duration) {
        Debug.Log("hit window");
        //PlayerControllerUI.SetActive(false);
        //Hammor.GetComponent<Animator>().SetBool(StaticVariables.BreakWindow, true);
        yield return new WaitForSeconds(duration);
        GlassWindow.SetActive(false);
        AllLevels[GameManager.instance.LevelSelected].Level.GetComponent<Level8handler>().BrakingPartical.SetActive(true);
        //yield return new WaitForSeconds(2);
        UIManager.Instance.ActivateWinPanel();
    }

    [System.Serializable]// Serialized Class of all levels
    public class Levels
    {
        [Header("Player")]
        [HideInInspector]public Transform PlayerPos;

        [Header("Level")]
        public GameObject Level;

        [Header("Enemy Positions")]
        [HideInInspector] public Transform EnemyStartPos;
        [HideInInspector] public Transform[] EnemyAIPos;

        
    }
}
