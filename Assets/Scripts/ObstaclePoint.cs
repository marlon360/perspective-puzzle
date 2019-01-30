using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePoint : MonoBehaviour {

    public Transform visibleObjectTransform;
    public Transform waitingTransform;

    private Vector3 hiddenObjectFocusPoint;
    private Vector3 visibleObjectFocusPoint;

    private Vector3 obstacleDirection;

    void Start () {
        this.hiddenObjectFocusPoint = transform.position + new Vector3 (0, GetComponent<BoxCollider> ().bounds.extents.y, 0);
        this.visibleObjectFocusPoint = this.visibleObjectTransform.position + new Vector3 (0, GetComponent<BoxCollider> ().bounds.extents.y, 0);

        this.obstacleDirection = this.visibleObjectFocusPoint - this.hiddenObjectFocusPoint;
        this.obstacleDirection = Vector3.Normalize (this.obstacleDirection);
    }

    public float rightCameraPerspective (Transform camera) {

        if (this.isPointVisibleInViewport (this.visibleObjectFocusPoint, Camera.main)) {
            Vector3 cameraDirection = camera.position - this.visibleObjectFocusPoint;
            cameraDirection = Vector3.Normalize (cameraDirection);

            return Vector3.Dot (obstacleDirection, cameraDirection);
        } else {
            return 0;
        }

    }

    private bool isPointVisibleInViewport (Vector3 point, Camera cam) {

        Vector3 viewportPoint = cam.WorldToViewportPoint (this.visibleObjectFocusPoint);

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

}