using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public ObstaclePoint[] points;
    public MoveTo playerMovement;
    public PlayerMessage playerMessage;
    public Transform endGoal;

    public float threshold = 50;

    public RiseWater riseWater;

    private int currentObstacleIndex = 0;
    private bool allObstaclesCompleted = false;
    private bool reachedEndGoal = false;

    void Start () {
        playerMovement.SetDestination (points[currentObstacleIndex].waitingTransform.position);
        points[currentObstacleIndex].IsNextObstacle = true;
        playerMessage.FadeInMessage("Bitte hilf mir zu dem Boot zu gelangen!");
    }

    // Update is called once per frame
    void Update () {
        if (!reachedEndGoal) {
            if (!allObstaclesCompleted) {
                int correctness = points[currentObstacleIndex].rightCameraPerspective ();

                if (correctness < this.threshold) {
                    this.moveToNextObstacle ();
                }
            } else {
                // when player reached the endpoint
                if (playerMovement.ReachedDestination ()) {
                    reachedEndGoal = true;
                    playerMovement.agent.enabled = false;
                    playerMovement.transform.parent = this.riseWater.boat;
                    this.riseWater.AccelerateRising();
                    // show message
                    playerMessage.FadeInMessage("Danke, dass du mich gerettet hast!", 3, 4);
                }
            }

            if(WaterIsAbovePlayer()) {
                Debug.Log("Player Dead, you lost");
            }
        }

    }

    private bool WaterIsAbovePlayer() {
        return riseWater.highestPointOfMesh.position.y > playerMovement.transform.position.y + (playerMovement.agent.height / 2);
    }

    private void moveToNextObstacle () {
        points[currentObstacleIndex].IsDone = true;
        currentObstacleIndex++;
        if (currentObstacleIndex < points.Length) {
            playerMovement.SetDestination (points[currentObstacleIndex].waitingTransform.position);
            points[currentObstacleIndex].IsNextObstacle = true;
        } else {
            allObstaclesCompleted = true;
            playerMovement.SetDestination (endGoal.position);
        }

        if(currentObstacleIndex == 1) {
            playerMessage.FadeOutMessage();
        }

        if(currentObstacleIndex == 3) {
            playerMessage.FadeInOutMessage("Wir haben es fast geschafft!");
        }

    }

}