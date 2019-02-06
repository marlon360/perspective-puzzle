using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public ObstaclePoint[] points;
    public MoveTo player;
    public Transform endGoal;

    public float threshold = 50;

    public RiseWater riseWater;

    private int currentObstacleIndex = 0;
    private bool allObstaclesCompleted = false;
    private bool reachedEndGoal = false;

    void Start () {
        player.SetDestination (points[currentObstacleIndex].waitingTransform.position);
    }

    // Update is called once per frame
    void Update () {
        if (!reachedEndGoal) {
            if (!allObstaclesCompleted) {
                // int correctness = points[currentObstacleIndex].rightCameraPerspective ();

                // if (correctness < this.threshold) {
                this.moveToNextObstacle ();
                //}
            } else {
                // when player reached the endpoint
                if (player.ReachedDestination ()) {
                    reachedEndGoal = true;
                    player.agent.enabled = false;
                    player.transform.parent = this.riseWater.boat;
                    this.riseWater.AccelerateRising();
                    // show message
                    Debug.Log("You Won");
                }
            }

            if(WaterIsAbovePlayer()) {
                Debug.Log("Player Dead, you lost");
            }
        }

    }

    private bool WaterIsAbovePlayer() {
        return riseWater.highestPointOfMesh.position.y > player.transform.position.y + (player.agent.height / 2);
    }

    private void moveToNextObstacle () {
        currentObstacleIndex++;
        if (currentObstacleIndex < points.Length) {
            player.SetDestination (points[currentObstacleIndex].waitingTransform.position);
        } else {
            allObstaclesCompleted = true;
            player.SetDestination (endGoal.position);
        }
    }

}