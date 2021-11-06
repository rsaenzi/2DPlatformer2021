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
    public float shootForce;
    public GameObject shootPivotRight;
    public GameObject shootPivotLeft;
    public GameObject levelStartPivot;

    bool marioHasShell = false;
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

            if (jumpsCount > 0) { // If Mario is already jumping...
                forceToApply = jumpForceOnAir;

            } else {
                if (marioIsMoving == true) { // If Mario is moving horizontally...
                    forceToApply = jumpForceMoving;

                } else { // If Mario is idle...
                    forceToApply = jumpForceIdle;
                }
            }

            this.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * forceToApply, ForceMode2D.Impulse);
            jumpsCount++;
        }


        // Shoot
        if (Input.GetKeyDown(KeyCode.LeftShift) && (marioHasShell == true)) {
            this.gameObject.GetComponent<Animator>().SetBool("MarioHasShell", false);
            marioHasShell = false;

            GameObject newShell = Instantiate(Resources.Load("Items/Shell") as GameObject);

            if (this.gameObject.GetComponent<SpriteRenderer>().flipX == true) {
                newShell.transform.position = shootPivotRight.transform.position;
                newShell.GetComponent<Rigidbody2D>().AddForce(Vector2.right * shootForce, ForceMode2D.Impulse);
            }
            if (this.gameObject.GetComponent<SpriteRenderer>().flipX == false) {
                newShell.transform.position = shootPivotLeft.transform.position;
                newShell.GetComponent<Rigidbody2D>().AddForce(Vector2.left * shootForce, ForceMode2D.Impulse);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {

        // Mario lands on a platform
        if (collision.gameObject.tag == "Platform") {
            this.gameObject.GetComponent<Animator>().SetBool("MarioIsJumping", false);
            jumpsCount = 0;
        }

        // Mario collects a shell
        if (collision.gameObject.tag == "Shell") {
            Destroy(collision.gameObject);
            this.gameObject.GetComponent<Animator>().SetBool("MarioHasShell", true);
            marioHasShell = true;
        }

        // Mario collects a coin
        if (collision.gameObject.tag == "Coin") {
            Destroy(collision.gameObject);
        }

        // Mario collects an egg
        if (collision.gameObject.tag == "Egg") {
            Destroy(collision.gameObject);
        }

        // Mario collects a heart
        if (collision.gameObject.tag == "Heart") {
            Destroy(collision.gameObject);
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

    void MarioDies() {
        //AudioPlayer.playSoundFX("MarioDies");
        this.transform.position = levelStartPivot.transform.position;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
    }
}