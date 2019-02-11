using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public ObstaclePoint[] points;
    public MoveTo playerMovement;
    public PlayerMessage playerMessage;
    public Transform endGoal;

    public float threshold = 50;

    public RiseWater riseWater;

    public UIManager uiManager;

    private int currentObstacleIndex = 0;
    private bool allObstaclesCompleted = false;
    private bool reachedEndGoal = false;

    private bool started = false;

    public void StartGame () {
        riseWater.StartRisingWater ();
        playerMovement.SetDestination (points[currentObstacleIndex].waitingTransform.position);
        points[currentObstacleIndex].IsNextObstacle = true;
        playerMessage.FadeInMessage ("Bitte hilf mir zu dem Boot zu gelangen!");
        started = true;
    }

    public void PauseGame() {
        riseWater.PauseRisingWater();
        playerMovement.StopMoving();
    }
    
    public void ContinueGame() {
        if(started) {
            riseWater.ContinueRisingWater();
            playerMovement.ContinueMoving();
        } else {
            StartGame();
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update () {
        if (!reachedEndGoal) {
            if (!allObstaclesCompleted) {
                // when player reached waiting position
                if (playerMovement.ReachedDestination ()) {
                    // mark next obstacle
                    points[currentObstacleIndex].IsNextObstacle = true;
                    // test correctness
                    int correctness = points[currentObstacleIndex].rightCameraPerspective ();
                    // if threshold reached move to next obstacle
                    if (correctness < this.threshold) {
                        this.moveToNextObstacle ();
                    }
                }
            } else {
                // when player reached the endpoint
                if (playerMovement.ReachedDestination ()) {
                    // set flag
                    reachedEndGoal = true;
                    // disable agent to move with boat
                    playerMovement.DisableAgent();
                    // set boat as parent to move player with boat
                    playerMovement.transform.parent = this.riseWater.boat;
                    // rise water to boat
                    this.riseWater.AccelerateRising ();
                    // show message
                    playerMessage.FadeInMessage ("Danke, dass du mich gerettet hast!", 5, 6);

                    uiManager.ShowVictory();
                }
            }

            if (WaterIsAbovePlayer ()) {
                uiManager.ShowGameOver();
                playerMovement.StopMoving();
            }
        }

    }

    private bool WaterIsAbovePlayer () {
        return riseWater.WaterHighestPoint().y > playerMovement.transform.position.y + (playerMovement.agent.height / 2);
    }

    private void moveToNextObstacle () {
        points[currentObstacleIndex].IsDone = true;
        currentObstacleIndex++;
        if (currentObstacleIndex < points.Length) {
            playerMovement.SetDestination (points[currentObstacleIndex].waitingTransform.position);
        } else {
            allObstaclesCompleted = true;
            playerMovement.SetDestination (endGoal.position);
        }

        if (currentObstacleIndex == 1) {
            playerMessage.FadeOutMessage ();
        }

        if (currentObstacleIndex == 3) {
            playerMessage.FadeInOutMessage ("Wir haben es fast geschafft!");
        }

    }

}