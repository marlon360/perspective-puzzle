using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public ObstaclePoint obstacle;

    private Renderer renderer;
    private float correctness = 0;

    public float minThreshold = 60;
    public float maxThreshold = 10;

    // Start is called before the first frame update
    void Start()
    {
        this.renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = obstacle.rightCameraPerspective();
        this.correctness = remap(distance, 60, 20, 0, 1);
        if(this.correctness < 0.5) {
            renderer.material.color = Color.Lerp(Color.red, Color.yellow, correctness * 2);
        }
        if(this.correctness >= 0.5) {
            renderer.material.color = Color.Lerp(Color.yellow, Color.green, (correctness - 0.5f) * 2);
        }
        
    }

    float remap(float oldValue, float oldMin, float oldMax, float newMin, float newMax) {
        float oldRange = (oldMax - oldMin);
        float newRange = (newMax - newMin); 
        float newValue = (((oldValue - oldMin) * newRange) / oldRange) + newMin;
        return newValue;
    }

}
