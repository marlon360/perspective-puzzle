using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;
using UnityEditor;

public class PlaceObject : MonoBehaviour {

    public GameObject Marker;
    public ARSessionOrigin SessionOrigin;
    public Camera ARCamera;

    public GameObject ObjectToPlace;

    Vector3 ScreenCenter;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit> ();

    private bool IsObjectPlaced = false;

    // Use this for initialization
    void Start () {
        ScreenCenter = new Vector3 (Screen.width / 2, Screen.height / 2, 0);
    }

    // Update is called once per frame
    void Update () {
        if (!IsObjectPlaced) {
            if (SessionOrigin.Raycast (ScreenCenter, s_Hits, TrackableType.PlaneWithinPolygon)) {
                Pose hitPose = s_Hits[0].pose;

                Marker.SetActive (true);
                Marker.transform.position = hitPose.position;
                Quaternion rotation = Quaternion.Euler (hitPose.rotation.eulerAngles.x, 0, hitPose.rotation.eulerAngles.z);
                Marker.transform.rotation = rotation;
            } else {
                Marker.SetActive (false);
            }

            if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
                placeObjectAtMarker ();
            }
        }

    }

    [ContextMenu("place")]
    private void placeObjectAtMarker () {
        ObjectToPlace.transform.position = Marker.transform.position;
        ObjectToPlace.transform.position = new Vector3(ObjectToPlace.transform.position.x, ObjectToPlace.transform.position.y + 0.2f, ObjectToPlace.transform.position.z);
        ObjectToPlace.transform.rotation = Marker.transform.rotation;
        ObjectToPlace.SetActive (true);
        Marker.SetActive (false);
        IsObjectPlaced = true;
    }

}