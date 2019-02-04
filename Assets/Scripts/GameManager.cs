using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour {

    public WayPoint[] points;
    public MoveTo player;

    public float threshold = 50;

    private Queue<WayPoint> pointsQueue = new Queue<WayPoint>();
    private bool allObstaclesCompleted = false;

    private WayPoint nextWayPoint;

    void Start () {
        foreach (WayPoint point in this.points) {
            this.pointsQueue.Enqueue (point);
        }
        WayPoint currentWayPoint = pointsQueue.Dequeue ();
        player.SetDestination (currentWayPoint.GetWayPoint ());
        this.nextWayPoint = pointsQueue.Dequeue();
    }

    // Update is called once per frame
    void Update () {
        if (!allObstaclesCompleted) {
            if (player.ReachedDestination ()) {
                this.moveToNextWayPoint();
            }
            //int correctness = points[currentObstacleIndex].rightCameraPerspective (mainCamera);

            // if (correctness < this.threshold) {
            //     this.moveToNextObstacle ();
            // }
        }
    }

    private void moveToNextWayPoint () {
        if (this.nextWayPoint.CanMoveTo ()) {
            player.SetDestination (nextWayPoint.GetWayPoint ());
            this.nextWayPoint = this.pointsQueue.Dequeue ();
            if(this.nextWayPoint == null) {
                allObstaclesCompleted = true;
            }
        }
    }

}