using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseWater : MonoBehaviour
{

    public Transform maximum;
    public float duration; //seconds

    public Transform highestPointOfMesh;

    private Vector3 targetScale;

    private float t;
    private Vector3 startScale;

    // Start is called before the first frame update
    void Start()
    {
        this.startScale = transform.localScale;

        float distanceToHighestMesh = highestPointOfMesh.position.y - transform.position.y;
        float distanceToMax = maximum.position.y - transform.position.y;

        float targetScaleY = (startScale.y / distanceToHighestMesh) * distanceToMax;

        this.targetScale = new Vector3(startScale.x, targetScaleY, startScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / this.duration;
        transform.localScale = Vector3.Lerp(startScale, targetScale, t);
    }
}
