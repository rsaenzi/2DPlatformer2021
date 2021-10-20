using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTurtle : MonoBehaviour {

    public float motionSpeed;
    public float distance;

    bool movingToRight = true;
    Vector3 initialPosition;
    SpriteRenderer spriteRenderer;


    void Start() {
        spriteRenderer = this.transform.GetComponent<SpriteRenderer>();

        initialPosition = this.transform.position;
        spriteRenderer.flipX = true;
    }

    void Update() {

        // Direction change
        if (movingToRight == true) {
            if (this.transform.position.x > initialPosition.x + distance) {
                movingToRight = false;
                spriteRenderer.flipX = false;
            }
        } else { // Moving Left
            if (this.transform.position.x < initialPosition.x - distance) {
                movingToRight = true;
                spriteRenderer.flipX = true;
            }
        }

        // Motion
        if (movingToRight == true) {
            this.transform.Translate(Vector3.right * motionSpeed * Time.deltaTime, Space.World);
        } else {
            this.transform.Translate(Vector3.left * motionSpeed * Time.deltaTime, Space.World);
        }
    }
}