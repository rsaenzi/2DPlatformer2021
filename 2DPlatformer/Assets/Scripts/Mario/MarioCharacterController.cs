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


    Text textCoins;
    Text textEggs;
    Slider healthBar;

    GameState state;
    Animator animatorMario;


    void Start() {
        textCoins = GameObject.Find("Canvas/PanelCoins/TextCoins").GetComponent<Text>();
        textEggs = GameObject.Find("Canvas/PanelEggs/TextEggs").GetComponent<Text>();
        healthBar = GameObject.Find("Canvas/SliderHealth").GetComponent<Slider>();

        state = GameObject.Find("GameState").GetComponent<GameState>();
        animatorMario = this.gameObject.GetComponent<Animator>();

        GameObject.Find("Music/Level1Grass").GetComponent<AudioSource>().Play();
    }


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

        animatorMario.SetBool("MarioIsRunning", marioIsMoving);


        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && (jumpsCount < maxJumpsAllowed)) {

            GameObject.Find("SoundFx/MarioJump").GetComponent<AudioSource>().Play();

            animatorMario.SetBool("MarioIsJumping", true);

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
            GameObject.Find("SoundFx/MarioShoot").GetComponent<AudioSource>().Play();
            animatorMario.SetBool("MarioHasShell", false);
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
            GameObject.Find("SoundFx/MarioLand").GetComponent<AudioSource>().Play();
            animatorMario.SetBool("MarioIsJumping", false);
            jumpsCount = 0;
        }

        // Mario collects a shell
        if (collision.gameObject.tag == "Shell") {
            Destroy(collision.gameObject);
            animatorMario.SetBool("MarioHasShell", true);
            marioHasShell = true;
        }

        // Mario collects a coin
        if (collision.gameObject.tag == "Coin") {
            GameObject.Find("SoundFx/Coin").GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);

            state.coins++;
            textCoins.text = state.coins.ToString();
        }

        // Mario collects an egg
        if (collision.gameObject.tag == "Egg") {
            GameObject.Find("SoundFx/Egg").GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);

            state.eggs++;
            textEggs.text = state.eggs.ToString();
        }

        // Mario collects a heart (+2)
        if (collision.gameObject.tag == "Heart") {
            GameObject.Find("SoundFx/Heart").GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);

            state.hearts += 2;
            healthBar.value = state.hearts;
        }

        // Mario is hurt by a Ghost (-1)
        if (collision.gameObject.tag == "Ghost") {
            GameObject.Find("SoundFx/MarioPain").GetComponent<AudioSource>().Play();

            state.hearts -= 1;
            healthBar.value = state.hearts;

            MarioDies();
        }

        // Mario is hurt by a Yellow Spike (-2)
        if (collision.gameObject.tag == "YellowSpike") {
            GameObject.Find("SoundFx/MarioPain").GetComponent<AudioSource>().Play();

            state.hearts -= 2;
            healthBar.value = state.hearts;

            MarioDies();
        }

        // Mario is hurt by a Blue Spike (-3)
        if (collision.gameObject.tag == "BlueSpike") {
            GameObject.Find("SoundFx/MarioPain").GetComponent<AudioSource>().Play();

            state.hearts -= 3;
            healthBar.value = state.hearts;

            MarioDies();
        }

        // Mario is hurt by a Red Turtle (-5)
        if (collision.gameObject.tag == "RedTurtle") {
            GameObject.Find("SoundFx/MarioPain").GetComponent<AudioSource>().Play();

            state.hearts -= 5;
            healthBar.value = state.hearts;

            MarioDies();
        }

        // Mario wins the level
        if (collision.gameObject.tag == "Door") {
            GameObject.Find("SoundFx/MarioWin").GetComponent<AudioSource>().Play();
            GameObject.Find("SoundFx/Door").GetComponent<AudioSource>().Play();
            GameObject.Find("Music/Level1Grass").GetComponent<AudioSource>().Stop();
        }

        // Mario falls from the level
        if (collision.gameObject.tag == "Fall") {
            GameObject.Find("SoundFx/MarioPain").GetComponent<AudioSource>().Play();

            state.hearts = 0;
            healthBar.value = state.hearts;

            MarioDies();
        }
    }


    void MarioDies() {

        // Mario Dies
        if (state.hearts <= 0) {
            GameObject.Find("SoundFx/MarioPain").GetComponent<AudioSource>().Play();
            this.gameObject.transform.position = levelStartPivot.transform.position;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;

            state.hearts = 10;
            healthBar.value = state.hearts;

            animatorMario.SetBool("MarioHasShell", false);
            marioHasShell = false;
        }
    }

}