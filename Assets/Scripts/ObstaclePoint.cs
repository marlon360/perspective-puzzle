using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstaclePoint : MonoBehaviour {

    public Transform waitingTransform;

    public Transform obstacleStartPoint;
    public Transform obstacleEndPoint;

    public Transform visibleStartPoint;
    public Transform visibleEndPoint;

    [HideInInspector]
    public bool IsNextObstacle = false;
    public bool IsDone = false;

    private Camera cam;

    private void Start() {
        this.cam = Camera.main;
    }

    public int rightCameraPerspective () {

        if (this.isPointVisibleInViewport (this.visibleStartPoint.position) &&
            this.isPointVisibleInViewport (this.visibleEndPoint.position) &&
            this.IsPointNearerInViewport (this.visibleStartPoint.position, this.obstacleStartPoint.position)) {

            Vector2 obstacleStartViewport = PointToViewportCoordinates (this.obstacleStartPoint.position);
            Vector2 obstacleEndViewport = PointToViewportCoordinates (this.obstacleEndPoint.position);

            Vector2 visibleStartViewport = PointToViewportCoordinates (this.visibleStartPoint.position);
            Vector2 visibleEndViewport = PointToViewportCoordinates (this.visibleEndPoint.position);

            float startPointDistance = Vector2.Distance (
                visibleStartViewport,
                obstacleStartViewport
            );

            float endPointDistance = Vector2.Distance (
                visibleEndViewport,
                obstacleEndViewport
            );

            return (int) ((startPointDistance + endPointDistance) / 2 * 1000);

        } else {
            return 1000000;
        }

    }

    private Vector2 PointToViewportCoordinates (Vector3 point) {
        Vector3 viewPortPoint = cam.WorldToViewportPoint (point);
        return new Vector2 (viewPortPoint.x, viewPortPoint.y);
    }

    private bool isPointVisibleInViewport (Vector3 point) {

        Vector3 viewportPoint = cam.WorldToViewportPoint (point);

        if (viewportPoint.z < 0) {
            return false;
        }

        if (viewportPoint.x < 0 || viewportPoint.x > 1) {
            return false;
        }

        if (viewportPoint.y < 0 || viewportPoint.y > 1) {
            return false;
        }

        return true;

    }

    private bool IsPointNearerInViewport (Vector3 pointA, Vector3 pointB) {
        Vector3 viewportPointA = cam.WorldToViewportPoint (pointA);
        Vector3 viewportPointB = cam.WorldToViewportPoint (pointB);
        return viewportPointA.z < viewportPointB.z;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(this.obstacleStartPoint.position, this.obstacleEndPoint.position);
        Gizmos.DrawLine(this.obstacleStartPoint.position, this.visibleStartPoint.position);
        Gizmos.DrawLine(this.obstacleEndPoint.position, this.visibleEndPoint.position);

    }

}