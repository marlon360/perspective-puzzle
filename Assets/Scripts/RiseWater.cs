using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RiseWater : MonoBehaviour {

    public Transform boat;
    public float duration; //seconds

    private float acceleration = 1;
    private bool isPaused = false;

    private BoxCollider col;

    private void Start() {
        this.col = GetComponent<BoxCollider>();
        StartRisingWater();
    }

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

        // Scale at Start
        Vector3 startScale = transform.localScale;

        // Position of the maximum bound
        Vector3 TopBoundOfWater = WaterHighestPoint();

        // distance from center to top of water
        float distanceToTopBound = TopBoundOfWater.y - transform.position.y;
        // distance from center to boat
        float distanceToBoat = boat.position.y - transform.position.y;

        // the Y-Scale value to reach the boat with the water 
        float targetScaleY = (startScale.y / distanceToTopBound) * distanceToBoat;

        // final scale: keep x- and z-Scale; new y-Scale plus offset
        Vector3 finalScale = new Vector3 (startScale.x, targetScaleY + offset, startScale.z);

        float elapsedTime = 0;
        while (elapsedTime < duration) {

            // update top bound position of water
            TopBoundOfWater = WaterHighestPoint();

            // lerp scale to finalscale
            transform.localScale = Vector3.Lerp (startScale, finalScale, (elapsedTime / duration));
            // when top of water is at boat, move boat with water
            if(TopBoundOfWater.y >= boat.position.y) {
                boat.position = new Vector3 (boat.position.x, TopBoundOfWater.y, boat.position.z);
            }
            // do not count time, when paused
            if(!isPaused) {
                elapsedTime += Time.deltaTime * acceleration;
            }
            yield return null;
        }
    }

    public Vector3 WaterHighestPoint() {
        return this.col.bounds.max;
    }

}