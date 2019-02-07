using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour {
    public ObstaclePoint obstacle;

    private Renderer rend;
    private float correctness = 0;

    public float minThreshold = 60;
    public float maxThreshold = 10;

    private AnimationCurve colorLerpCurve;

    // Start is called before the first frame update
    void Start () {
        this.rend = GetComponent<Renderer> ();
        this.rend.material.color = Color.white;
        this.colorLerpCurve = new AnimationCurve (
            new Keyframe (0, 0),
            new Keyframe (0.5f, 0.05f),
            new Keyframe (0.8f, 0.1f),
            new Keyframe (0.95f, 0.2f),
            new Keyframe (1, 1)
        );
    }

    // Update is called once per frame
    void Update () {
        if(this.obstacle.IsDone) {
            rend.material.color = Color.green;
        } else if (this.obstacle.IsNextObstacle) {
            float distance = obstacle.rightCameraPerspective ();
            this.correctness = remap (distance, minThreshold, maxThreshold, 0, 1);
            if (this.correctness >= 0.5) {
                rend.material.color = Color.Lerp (Color.yellow, Color.green, this.colorLerpCurve.Evaluate ((correctness - 0.5f) * 2));
            } else if (this.correctness > 0) {
                rend.material.color = Color.Lerp (Color.red, Color.yellow, correctness * 2);
            } else {
                rend.material.color = Color.white;
            }
        }
    }

    float remap (float oldValue, float oldMin, float oldMax, float newMin, float newMax) {
        float oldRange = (oldMax - oldMin);
        float newRange = (newMax - newMin);
        float newValue = (((oldValue - oldMin) * newRange) / oldRange) + newMin;
        return newValue;
    }

}