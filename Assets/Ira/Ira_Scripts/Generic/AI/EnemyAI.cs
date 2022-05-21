using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Agent")]
    public UnityEngine.AI.NavMeshAgent Agent;//Agent is Teacher

    [Header("Repeat Rate")]
    [SerializeField] float _decisionDelay = 0.5f;//repeat rate of SetDestination Method

    public float Distance;//Distance Between the Teacher and the Player

    public float CatchDistance;

    [SerializeField] Transform _objectToChase;// The object which teacher have to chase
    [SerializeField] Transform[] _wayPoints;//The Points on which the teacher will move

    public int _currentWaypoint = 0;//Current Position which teacher have to reach

    private Animator _animator;//Animator of Teacher

    private bool _roundCompleted = false;//The round of covering all the wayPoints

    public EnemyStates _currentState;// THe state in which the teacher is currently in


    private bool IsMainMenuMusicON = false;
    private bool IsGamePlayMusicON = false;
    void Start()
    {

        if (transform.gameObject.activeSelf)//Check if the GameObject of teacher is active?
        {
            _animator = GetComponent<Animator>();//Get the Animator of the teacher
            Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();//Get the NavMeshAgent of the teacher
            if (GamePlayHandler.Instance.AllLevels[GameManager.instance.LevelSelected].EnemyAIPos.Length != 0)//Check if the teacher has positions to move or not
            {
                _wayPoints = GamePlayHandler.Instance.AllLevels[GameManager.instance.LevelSelected].EnemyAIPos;
                if (_currentState == EnemyStates.Patrolling)
                    Agent.SetDestination(_wayPoints[_currentWaypoint].position);//Give teacher the positon of current wayPoint
                InvokeRepeating("StartChasing", 0.5f, _decisionDelay);//Calls the SetDestination Method repeatedly 
            }
        }


    }

    void Update()
    {
        if (transform.gameObject.activeSelf)
        {
            //Debug.Log(Vector3.Distance(transform.position, _objectToChase.position));

            //Debug.Log("Enemy Y Pos" + this.transform.localPosition.y);
            //Debug.Log("Player Y Pos" + _objectToChase.transform.localPosition.y);
            //Debug.Log(Mathf.Abs(this.transform.localPosition.y - _objectToChase.transform.localPosition.y));

            if (Vector3.Distance(transform.position, _objectToChase.position) > Distance)//Distance Between Player and Teacher
            {
                if(!IsMainMenuMusicON)
                {
                    IsMainMenuMusicON = true;
                    IsGamePlayMusicON = false;
                    SoundManager.instance.PlayBackgroundMusic(AudioClipsSource.Instance.MainMenuClip);
                }

                UIManager.Instance.DoctorCam.SetActive(false);
                _currentState = EnemyStates.Patrolling;
                Agent.speed = 1.8f;
                _animator.SetBool("Chasing", false);//Start the Wandering Animation
                _animator.SetBool("Patrolling", true);
            }
            else 
            {
                if(!GamePlayHandler.Instance.PlayerDie && Mathf.Abs(this.transform.localPosition.y - _objectToChase.transform.localPosition.y) < 2)
                {
                    if(!IsGamePlayMusicON)
                    {
                        IsGamePlayMusicON = true;
                        IsMainMenuMusicON = false;
                        SoundManager.instance.MusicVolume = 1;
                        SoundManager.instance.PlayBackgroundMusic(AudioClipsSource.Instance.GamePlayClip);
                    }
                    
                    UIManager.Instance.DoctorCam.SetActive(true);
                    _currentState = EnemyStates.Chasing;
                    Agent.speed = 2.2f;
                    _animator.SetBool("Patrolling", false);
                    _animator.SetBool("Chasing", true);//Start the Chasing Animation
                }
                else
                {
                    if(EnemyController.Instance.AgainPatrolling)
                    {
                        if(!IsMainMenuMusicON)
                        {
                            IsMainMenuMusicON = true;
                            IsGamePlayMusicON = false;
                            SoundManager.instance.PlayBackgroundMusic(AudioClipsSource.Instance.MainMenuClip);
                        }
                        
                        _currentState = EnemyStates.Patrolling;
                        Agent.speed = 1.8f;
                        _animator.SetBool("Chasing", false);
                        _animator.SetBool("Patrolling", true);
                    }
                    else if (_animator.GetBool(StaticVariables.EnemyAttack))
                    {
                        _animator.SetBool("Chasing", false);
                        _animator.SetBool("Patrolling", false);
                    }
                    
                }
                
            }

            if (_wayPoints.Length != 0)//Check if the teacher has positions to move or not
            {
                if (_currentState == EnemyStates.Patrolling)
                {
                    //Check if the Teacher Reaches the current wayPoint then give it the next wayPoint
                    if (Vector3.Distance(transform.position, _wayPoints[_currentWaypoint].position) <= 2)
                    {

                        if (_roundCompleted == false)
                        {
                            _currentWaypoint++;//Give the next wayPoint
                        }
                        else
                        {
                            _currentWaypoint--;//Give the previous wayPoint
                        }

                        if (_currentWaypoint == _wayPoints.Length)//If teacher is at the last wayPoint
                        {
                            _currentWaypoint--;
                            _roundCompleted = true;//round Completed will be true if the teacher is moving from last position to first position
                        }

                        if (_currentWaypoint < 0)//If teacher is at the first wayPoint
                        {
                            _currentWaypoint++;
                            _roundCompleted = false;//round Completed will be false if the teacher is moving from first position to last position
                        }


                    }
                    Agent.SetDestination(_wayPoints[_currentWaypoint].position);//Give teacher the positon of current wayPoint
                }

            }
            if (Vector3.Distance(transform.position, _objectToChase.position) <= CatchDistance&&!GamePlayHandler.Instance.PlayerDie/*&&EnemyController.Instance.IsTowardsPlayer*/)//Distance Between Player and Teacher
            {
                _animator.SetBool("Patrolling", false);
                _animator.SetBool("Chasing", false);
                Agent.speed = 0;
                GamePlayHandler.Instance.PlayerControllerUI.SetActive(false);
                GamePlayHandler.Instance.PlayerDie = true;
                this.GetComponent<Animator>().SetBool(StaticVariables.EnemyAttack, true);
                //SoundManager.instance.PlayEffect(AudioClipsSource.Instance.GhostSound);

            }

        }
    }

    void GiveTeacherPosition()
    {
        Agent.SetDestination(_wayPoints[_currentWaypoint].position);
    }
    void StartChasing()//Start Chasing the Player
    {
        if(this.gameObject.activeSelf)
        {
            if (_currentState == EnemyStates.Chasing)
                Agent.SetDestination(_objectToChase.position);
        }
        
    }
    public enum EnemyStates//States of Teacher
    {
        Catch,
        Patrolling,
        Chasing
    }
}
