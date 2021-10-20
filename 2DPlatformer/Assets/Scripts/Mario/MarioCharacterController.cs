using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarioCharacterController : MonoBehaviour {

    public float motionSpeed;
    public float jumpForce;


    void Update() {

        // Motion
        if (Input.GetKey(KeyCode.RightArrow)) {
            this.transform.Translate(Vector2.right * motionSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            this.transform.Translate(Vector2.left * motionSpeed * Time.deltaTime);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space)) {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
        }
    }
}