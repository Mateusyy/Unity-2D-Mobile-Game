using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform target;
    private float sTime = 0.05f;
    private Vector3 velocity = Vector3.zero;

    [HideInInspector]
    public float minBallYPos;

    private void Start() {
        target = FindObjectOfType<Ball>().transform;
    }

    private void Update() {
        CheckPositionBallY();

        if (target.position.y-1f < transform.position.y) {
            Vector3 targetPosition = new Vector3(transform.position.x, minBallYPos, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, sTime);
        }
    }

    void CheckPositionBallY() {
        if (target.position.y < minBallYPos) {
            minBallYPos = target.position.y;
        }
    }
}

