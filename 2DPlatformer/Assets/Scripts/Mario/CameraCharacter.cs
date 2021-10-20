using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraCharacter : MonoBehaviour {

    [Tooltip("GameObject to be followed by the camera")]
    public Transform targetToFollow;

    // This variable will hold the distance in X between the character and the camera
    private float distanceInX;

    // Variable used for interpolation
    Vector3 targetPosition;


    void Awake() {
        distanceInX = this.transform.position.x - targetToFollow.position.x;
        this.transform.position = new Vector3(targetToFollow.position.x + distanceInX, 0, -50);
    }

    void LateUpdate() {
        if (targetToFollow != null) {
            targetPosition = new Vector3(targetToFollow.position.x + distanceInX, 0, -50);
            this.transform.position = Vector3.Slerp(this.transform.position, targetPosition, 0.04f);
        }
    }
}