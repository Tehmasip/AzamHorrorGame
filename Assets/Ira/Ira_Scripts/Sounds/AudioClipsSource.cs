using UnityEngine;


public class AudioClipsSource : MonoBehaviour
{
    public static AudioClipsSource Instance;

    [Header("Music Clips")]
    public AudioClip MainMenuClip;
    public AudioClip GamePlayClip;
    public AudioClip GamePlayClip2;

    public AudioClip GenericButtonClick;
    public AudioClip genericButtonClickBack;
    public AudioClip GenericButtonClick2;

    public AudioClip LevelFailedClip;
    public AudioClip LevelSuccessClip;

    public AudioClip GetHurtSound;
    public AudioClip EmergencyAlarm;
    public AudioClip GhostSound;
    public AudioClip OhMyGodSound;
    public AudioClip BreatingHeavily;
    public AudioClip PickUpSound;
    public AudioClip LiftDoorOpenOrClose;
    public AudioClip LiftGoesUp;
    public AudioClip DoorOpen;
    public AudioClip DrawerOpen;
 

    public AudioClip PlayerWalkSound;
    public AudioClip PlayerRunSound;
    public AudioClip PlayerJumpSound;

    public AudioClip CodeEnterSound;
    public AudioClip WrongCode;
    public AudioClip FallSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
