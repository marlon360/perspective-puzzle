using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public ObstaclePoint[] points;
    public MoveTo player;
    public Transform endGoal;

    public float threshold = 50;

    private Camera mainCamera;
    private int currentObstacleIndex = 0;
    private bool allObstaclesCompleted = false;

    void Start () {
        this.mainCamera = Camera.main;
        player.SetDestination (points[currentObstacleIndex].waitingTransform.position);
    }

    // Update is called once per frame
    void Update () {
        if (!allObstaclesCompleted) {
            int correctness = points[currentObstacleIndex].rightCameraPerspective (mainCamera);

            if (correctness < this.threshold) {
                this.moveToNextObstacle ();
            }
        }
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