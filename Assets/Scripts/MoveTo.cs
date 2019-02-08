using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour {

    [HideInInspector]
    public NavMeshAgent agent;
    
    private Animator anim;

    void Start () {
        this.agent = GetComponent<NavMeshAgent> ();
        this.anim = GetComponent<Animator> ();
        // set to grounded
        this.anim.SetBool ("Grounded", true);
    }

    void Update () {
        // update speed in animator with agent speed
        this.anim.SetFloat ("MoveSpeed", this.agent.velocity.magnitude / this.agent.speed / 1.6f);
    }

    public void SetDestination (Vector3 destination) {
        if (this.agent == null) {
            this.agent = GetComponent<NavMeshAgent> ();
        }
        this.agent.destination = destination;
    }

    public bool ReachedDestination () {
        if (!agent.pathPending) {
            if (agent.remainingDistance <= agent.stoppingDistance) {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
                    return true;
                }
            }
        }
        return false;
    }

    public void StopMoving() {
        this.agent.isStopped = true;
    }

    public void ContinueMoving() {
        this.agent.isStopped = false;
    }

    public void DisableAgent() {
        this.agent.enabled = false;
    }

}