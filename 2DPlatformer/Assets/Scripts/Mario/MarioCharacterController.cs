using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarioCharacterController : MonoBehaviour {

    public float motionSpeed;
    public float jumpForceIdle;
    public float jumpForceMoving;
    public float jumpForceOnAir;
    public int maxJumpsAllowed;

    bool marioIsMoving = false;
    int jumpsCount = 0;


    void Update() {

        // Motion
        marioIsMoving = false;

        if (Input.GetKey(KeyCode.RightArrow)) {
            this.transform.Translate(Vector2.right * motionSpeed * Time.deltaTime, Space.World);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            marioIsMoving = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            this.transform.Translate(Vector2.left * motionSpeed * Time.deltaTime, Space.World);
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            marioIsMoving = true;
        }

        this.gameObject.GetComponent<Animator>().SetBool("MarioIsRunning", marioIsMoving);


        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && (jumpsCount < maxJumpsAllowed)) {
            this.gameObject.GetComponent<Animator>().SetBool("MarioIsJumping", true);

            float forceToApply = 0.0f;

            // If Mario is already jumping...
            if (jumpsCount > 0) {
                forceToApply = jumpForceOnAir;

            } else {
                // If Mario is moving horizontally...
                if (marioIsMoving == true) {
                    forceToApply = jumpForceMoving;

                } else { // If Mario is idle...
                    forceToApply = jumpForceIdle;
                }
            }

            this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * forceToApply, ForceMode2D.Impulse);
            jumpsCount++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {

        // Mario lands on a platform
        if (collision.gameObject.tag == "Platform") {
            this.gameObject.GetComponent<Animator>().SetBool("MarioIsJumping", false);
            jumpsCount = 0;
        }

        // Mario collects a coin
        if (collision.gameObject.tag == "Coin") {

        }

        // Mario collects an egg
        if (collision.gameObject.tag == "Egg") {

        }

        // Mario collects a heart
        if (collision.gameObject.tag == "Heart") {

        }

        // Mario is hurt by a Ghost
        if (collision.gameObject.tag == "Ghost") {

        }

        // Mario is hurt by a Yellow Spike
        if (collision.gameObject.tag == "YellowSpike") {

        }

        // Mario is hurt by a Blue Spike
        if (collision.gameObject.tag == "BlueSpike") {

        }

        // Mario is hurt by a Red Turtle
        if (collision.gameObject.tag == "RedTurtle") {

        }

        // Mario wins the level
        if (collision.gameObject.tag == "Door") {

        }

        // Mario falls from the level
        if (collision.gameObject.tag == "Fall") {

        }
    }
}