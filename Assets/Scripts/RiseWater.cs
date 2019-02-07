using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RiseWater : MonoBehaviour {

    public Transform boat;
    public float duration; //seconds

    public Transform highestPointOfMesh;

    private Vector3 finalScale;

    private Coroutine WaterRiseCoroutine;

    private float acceleration = 1;

    private bool isPaused = false;

    public void StartRisingWater() {
        StartCoroutine (RiseWaterWithBoat (this.duration, 3));
    }

    public void PauseRisingWater() {
        isPaused = true;
    }

    public void ContinueRisingWater() {
        isPaused = false;
    }

    public void AccelerateRising(float acceleration = 20f) {
        this.acceleration = acceleration;
    }


    private IEnumerator RiseWaterWithBoat (float duration, float offset) {

        Vector3 startScale = transform.localScale;

        float distanceToHighestMesh = highestPointOfMesh.position.y - transform.position.y;
        float distanceToBoat = boat.position.y - transform.position.y;

        float targetScaleY = (startScale.y / distanceToHighestMesh) * distanceToBoat;

        this.finalScale = new Vector3 (startScale.x, targetScaleY + offset, startScale.z);

        float elapsedTime = 0;
        while (elapsedTime < duration) {

            transform.localScale = Vector3.Lerp (startScale, finalScale, (elapsedTime / duration));
            if(this.highestPointOfMesh.position.y >= boat.position.y) {
                boat.position = new Vector3 (boat.position.x, this.highestPointOfMesh.position.y, boat.position.z);
            }
            if(!isPaused) {
                elapsedTime += Time.deltaTime * acceleration;
            }
            yield return null;
        }
    }

}