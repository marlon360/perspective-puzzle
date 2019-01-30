using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public ObstaclePoint[] points;
    public MoveTo player;
    public Transform endGoal;

    public float threshold = 0.00001f;

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
            float correctness = points[currentObstacleIndex].rightCameraPerspective (mainCamera.transform);

            if (correctness + this.threshold > 1) {
                this.moveToNextObstacle ();
                Debug.Log ("MOVE");
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