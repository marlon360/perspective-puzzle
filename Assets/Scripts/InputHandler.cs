using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class InputHandler : MonoBehaviour {

    public ARSessionOrigin SessionOrigin;

    public float rotationSensitivity = 0.4f;
    public float translationSensitivity = 0.001f;

    private Vector3 reference;
    private Vector3 offset;

    private Vector3 rotation = Vector3.zero;
    private Vector3 translation = Vector3.zero;

    private bool isDragging;

    void Update () {

        if (Input.GetMouseButtonDown (0)) {
            // rotating flag
            isDragging = true;

            // store mouse
            reference = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp (0)) {
            // rotating flag
            isDragging = false;
        }

        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch (0);
            if (touch.phase == TouchPhase.Began) {
                // rotating flag
                isDragging = true;

                // store touch
                reference = touch.position;
            }

            if (touch.phase == TouchPhase.Ended) {
                // rotating flag
                isDragging = false;
            }

            if (Input.GetTouch (0).phase == TouchPhase.Stationary || Input.GetTouch (0).phase == TouchPhase.Moved) {
                offset = (touch.position - new Vector2 (reference.x, reference.y));
                // store touch
                reference = touch.position;
            }
        } else {
            // offset
            offset = -(Input.mousePosition - reference);
            // store mouse
            reference = Input.mousePosition;
        }

        if (isDragging) {

            // apply rotation
            rotation.y = offset.x * rotationSensitivity;
            SessionOrigin.transform.Rotate(rotation);

            // apply translation
            translation.y = offset.y * translationSensitivity;

            SessionOrigin.MakeContentAppearAt(transform, translation);

        }
    }
}