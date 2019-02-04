using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{

    public Vector3 point = new Vector3(0,1,0);


    public Vector3 GetWayPoint() {
        return transform.position + point;
    }

    public virtual bool CanMoveTo() {
        return true;
    }

    protected virtual void OnDrawGizmos() {
        Gizmos.DrawSphere(GetWayPoint(), 0.01f);
    }

    void Reset()
    {
        BoxCollider col = GetComponent<BoxCollider>();
        point = new Vector3(0,col.bounds.extents.y * 2,0);
    }

}
