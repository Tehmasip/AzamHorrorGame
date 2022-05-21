using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Singleton of GamePlay Class
    #region Singleton
    public static EnemyController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    private Animator EnemyAnimator;

    public bool IsEnemyAttacking=false;
    public bool IsTowardsPlayer = false;
    public bool AgainPatrolling = false;
    private void Start()
    {
        EnemyAnimator = this.GetComponent<Animator>();
    }

    public void TurnAttackOFF(int n)
    {
        EnemyAnimator.SetBool(StaticVariables.EnemyAttack, false);
        SoundManager.instance.PlayEffect(AudioClipsSource.Instance.GhostSound);
        GamePlayHandler.Instance.PlayerControllerUI.SetActive(false);
        GamePlayHandler.Instance.PlayerFaintAnimation.gameObject.SetActive(true);
        GamePlayHandler.Instance.PlayerFaintAnimation.Play();
        StartCoroutine(OnEndOfPlayerFaintAnimation(GamePlayHandler.Instance.PlayerFaintAnimation.duration));
        StartCoroutine(OnMidOfPlayerDeathAnimation(GamePlayHandler.Instance.PlayerFaintAnimation.duration));
    }

    IEnumerator OnMidOfPlayerDeathAnimation(double delay)
    {
        yield return new WaitForSeconds((float)delay/2);
        AgainPatrolling = true;
    }
    IEnumerator OnEndOfPlayerFaintAnimation(double delay)
    {
        yield return new WaitForSeconds((float)delay);
        UIManager.Instance.EnableFailPanel(true);
        //if(GameManager.instance.LevelSelected==0&&GamePlayHandler.Instance.PickItem)
        //{
        //    //UIManager.Instance.EnableWinPanel(true);
        //}
        //else
        //{
        //    //Loose Panel
        //    //UIManager.Instance.EnableFailPanel(true);
        //}
    }
}
