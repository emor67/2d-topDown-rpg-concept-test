using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.1f; // How quickly the camera will move to its target position
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Offset the camera from the target

    private Transform _transform;
    private Vector3 _velocity;

    void Awake()
    {
        _transform = transform; // Cache the transform component
    }

    void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        // Calculate the desired position of the camera based on the target's position and offset
        Vector3 desiredPosition = target.position + offset;

        // Use Vector3.SmoothDamp to smoothly move the camera to its desired position
        Vector3 smoothedPosition = Vector3.SmoothDamp(_transform.position, desiredPosition, ref _velocity, smoothTime * Time.fixedDeltaTime);

        // Update the camera's position to the smoothed position, but keep the Z position at -10 to ensure it remains in the 2D plane
        _transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10f);
    }
}

