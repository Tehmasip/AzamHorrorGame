using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendCompenion : MonoBehaviour
{
    public Transform Player;
    NavMeshAgent Agent;
    Animator anim;
    public float idlespeed = 2, Walkspeed = 2, runspeed = 4, IdledistanceArea = 3;
    void Start()
    {
        transform.parent = null;
        Player = GamePlayHandler.Instance.Player.transform;
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("idle");
    }

    // Update is called once per frame
    void Update()
    {
        if(Player != null)
        {
            if (Vector3.Distance(transform.position,Player.position) > IdledistanceArea && Vector3.Distance(transform.position, Player.position) < 20)
            {
                Debug.Log("Walk");
                Agent.speed = Walkspeed;
                anim.ResetTrigger("idle");
                anim.SetTrigger("walk");
                
                Agent.SetDestination(Player.position);
                transform.LookAt(Player);

                
            }
            else if (Vector3.Distance(transform.position, Player.position) >= 20)
            {
                Debug.Log("Walk");
                Agent.speed = runspeed;
                anim.ResetTrigger("idle");
                anim.ResetTrigger("walk");
                anim.SetTrigger("run");

                Agent.SetDestination(Player.position);
                transform.LookAt(Player);


            }
            else
            {
                Debug.Log("idle");
                Agent.speed = idlespeed;
                anim.ResetTrigger("walk");
                anim.SetTrigger("idle");
                
                Agent.SetDestination(transform.position);
                transform.LookAt(Player);
            }
        }
    }
}
