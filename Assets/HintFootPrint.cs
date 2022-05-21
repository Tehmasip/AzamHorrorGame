using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HintFootPrint : MonoBehaviour
{
    public Transform Player;
    public Transform target;
    NavMeshAgent Agent;
    public Animator anim;
    public float speed=2,TargetArea= 3,playerDistanceStopArea = 10;
    public bool stop;
    // Start is called before the first frame update
    void Start()
    {
        Player =  GamePlayHandler.Instance.Player.transform;
        Agent = GetComponent<NavMeshAgent>();
        anim = anim.GetComponent<Animator>();
        Agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Player !=null && target != null && stop ==false)
        {
            if (Vector3.Distance(transform.position, target.position) < TargetArea)
            {
                stop = true;
            }
            if (Vector3.Distance(transform.position, Player.position) > playerDistanceStopArea)
            {
                anim.SetBool("play", false);
                //anim.enabled = false;
                Agent.SetDestination(transform.position);
            }
            else
            {
                anim.SetBool("play", true);
                Agent.SetDestination(target.position);
                //anim.enabled = true;
            }
        }
    }

}
