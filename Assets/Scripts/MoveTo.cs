using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour {

    private NavMeshAgent agent;

    void Start () {
        this.agent = GetComponent<NavMeshAgent> ();
    }

    public void SetDestination (Vector3 destination) {
        if (this.agent == null) {
            this.agent = GetComponent<NavMeshAgent> ();
        }
        this.agent.destination = destination;
    }

}