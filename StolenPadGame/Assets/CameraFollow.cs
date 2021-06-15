using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    Vector3 targetPosition;

    Vector3 tempVec3 = new Vector3();

    private void FixdedUpdate()
    {
        targetPosition.x = target.position.x;
        targetPosition.y = transform.position.y;
        targetPosition.z = transform.position.z;

        Vector3 desiredPosition = targetPosition + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition,
            smoothSpeed);
        transform.position = smoothedPosition;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, transform.position.y,
            transform.position.z);
    }
}
