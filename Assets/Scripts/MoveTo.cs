using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour {

    public float speed = 2f;

    private Rigidbody rigid;
    private Vector3 destination;

    private Vector3 direction;
    private float lastSqrMag;

    private bool reachedDestination = false;

    private void Start () {
        this.rigid = GetComponent<Rigidbody> ();
    }

    void Update () {
        if (!reachedDestination && destination != null) {
            // check the current sqare magnitude
            float sqrMag = (destination - transform.position).sqrMagnitude;

            // check this against the lastSqrMag
            // if this is greater than the last,
            // rigidbody has reached target and is now moving past it
            if (sqrMag > lastSqrMag) {
                // rigidbody has reached target and is now moving past it
                // stop the rigidbody by setting the velocity to zero
                direction = Vector3.zero;
                reachedDestination = true;
            }

            // make sure you update the lastSqrMag
            lastSqrMag = sqrMag;

            Vector3 targetPoint = destination;
            Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * 8.0f);

        }
    }

    void FixedUpdate () {
        rigid.MovePosition (transform.position + direction * Time.deltaTime * speed);
    }

    public void SetDestination (Vector3 destination) {
        this.destination = destination;
        this.direction = (this.destination - transform.position).normalized;
        this.lastSqrMag = Mathf.Infinity;
        this.reachedDestination = false;
    }

    public bool ReachedDestination () {
        return this.reachedDestination;
    }

}