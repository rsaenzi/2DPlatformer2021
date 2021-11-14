using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarioCharacterController : MonoBehaviour {

    public float motionSpeed;
    public float shootForce;
    public float jumpForce;

    public GameObject shootPivotRight;
    public GameObject shootPivotLeft;
    public GameObject levelStartPivot;

    float keyHorizontalValue = 0.0f;

    bool marioHasShell = false;
    bool marioIsMoving = false;
    bool marioIsJumping = false;

    Text textCoins;
    Text textEggs;
    Slider healthBar;

    GameState state;
    GameObject shellPrefab;
    Animator animatorMario;
    SpriteRenderer rendererMario;
    Rigidbody2D rigidbodyMario;


    void Start() {

        textCoins = GameObject.Find("Canvas/ScreenHUD/PanelCoins/TextCoins").GetComponent<Text>();
        textEggs = GameObject.Find("Canvas/ScreenHUD/PanelEggs/TextEggs").GetComponent<Text>();
        healthBar = GameObject.Find("Canvas/ScreenHUD/SliderHealth").GetComponent<Slider>();

        state = GameObject.Find("GameState").GetComponent<GameState>();
        shellPrefab = Resources.Load("Items/Shell") as GameObject;
        animatorMario = this.gameObject.GetComponent<Animator>();
        rendererMario = this.gameObject.GetComponent<SpriteRenderer>();
        rigidbodyMario = this.gameObject.GetComponent<Rigidbody2D>();
    }


    void Update() {

        // Jump
        if ((Mathf.Abs(Input.GetAxis("Jump")) > 0.001f) && (marioIsJumping == false)) {
            AudioPlayer.playSoundFX("MarioJump");
            animatorMario.SetBool("MarioIsJumping", true);
            rigidbodyMario.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            marioIsJumping = true;
        }


        // Shoot Shell
        if ((Mathf.Abs(Input.GetAxis("Fire1")) > 0.001f) && (marioHasShell == true)) {
            AudioPlayer.playSoundFX("MarioShoot");
            animatorMario.SetBool("MarioHasShell", false);
            marioHasShell = false;

            GameObject newShell = Instantiate(shellPrefab);

            if (rendererMario.flipX == true) {
                newShell.transform.position = shootPivotRight.transform.position;
                newShell.GetComponent<Rigidbody2D>().AddForce(Vector2.right * shootForce, ForceMode2D.Impulse);
            }
            if (rendererMario.flipX == false) {
                newShell.transform.position = shootPivotLeft.transform.position;
                newShell.GetComponent<Rigidbody2D>().AddForce(Vector2.left * shootForce, ForceMode2D.Impulse);
            }
        }
    }

    void FixedUpdate() {

        // Motion
        marioIsMoving = false;
        keyHorizontalValue = Input.GetAxis("Horizontal");

        if (Mathf.Abs(keyHorizontalValue) > 0.001f) {
            marioIsMoving = true;
            rigidbodyMario.velocity = new Vector2(keyHorizontalValue * motionSpeed, rigidbodyMario.velocity.y); ;

            // Look to Right or Left
            if (keyHorizontalValue > 0.0f) {
                rendererMario.flipX = true;
            } else {
                rendererMario.flipX = false;
            }
        }

        animatorMario.SetBool("MarioIsRunning", marioIsMoving);
    }

    void OnCollisionEnter2D(Collision2D collision) {

        // Mario lands on a platform
        if (collision.gameObject.tag == "Platform") {
            AudioPlayer.playSoundFX("MarioLand");
            animatorMario.SetBool("MarioIsJumping", false);
            marioIsJumping = false;
        }

        // Mario collects a shell
        if (collision.gameObject.tag == "Shell") {
            AudioPlayer.playSoundFX("MarioCollect");
            Destroy(collision.gameObject);
            animatorMario.SetBool("MarioHasShell", true);
            marioHasShell = true;
        }

        // Mario collects a coin
        if (collision.gameObject.tag == "Coin") {
            AudioPlayer.playSoundFX("Coin");
            Destroy(collision.gameObject);

            state.coins++;
            textCoins.text = state.coins.ToString();
        }

        // Mario collects an egg
        if (collision.gameObject.tag == "Egg") {
            AudioPlayer.playSoundFX("Egg");
            Destroy(collision.gameObject);

            state.eggs++;
            textEggs.text = state.eggs.ToString();
        }

        // Mario collects a heart (+2 health point)
        if (collision.gameObject.tag == "Heart") {
            AudioPlayer.playSoundFX("Heart");
            Destroy(collision.gameObject);

            state.hearts += 2;
            healthBar.value = state.hearts;
        }

        // Mario is hurt by a Ghost (-1 health point)
        if (collision.gameObject.tag == "Ghost") {
            AudioPlayer.playSoundFX("MarioPain");

            state.hearts -= 1;
            healthBar.value = state.hearts;
            MarioDies();
        }

        // Mario is hurt by a Yellow Spike (-2 health point)
        if (collision.gameObject.tag == "YellowSpike") {
            AudioPlayer.playSoundFX("MarioPain");

            state.hearts -= 2;
            healthBar.value = state.hearts;
            MarioDies();
        }

        // Mario is hurt by a Blue Spike (-3 health point)
        if (collision.gameObject.tag == "BlueSpike") {
            AudioPlayer.playSoundFX("MarioPain");

            state.hearts -= 3;
            healthBar.value = state.hearts;
            MarioDies();
        }

        // Mario is hurt by a Red Turtle (-5 health point)
        if (collision.gameObject.tag == "RedTurtle") {
            AudioPlayer.playSoundFX("MarioPain");

            state.hearts -= 5;
            healthBar.value = state.hearts;
            MarioDies();
        }

        // Mario falls from the level
        if (collision.gameObject.tag == "Fall") {
            AudioPlayer.playSoundFX("MarioPain");

            state.hearts = 0;
            healthBar.value = state.hearts;
            MarioDies();
        }

        // Mario wins the level
        if (collision.gameObject.tag == "Door") {
            AudioPlayer.stopAllMusic();
            AudioPlayer.playSoundFX("MarioWin");
            AudioPlayer.playSoundFX("Door");
            Invoke("playMusicUINavigation", 5);

            UINavigation.changeScreen("ScreenHUD", "ScreenWin");
            LevelLoader.unloadAllLevels();
            GameObject.Find("CameraUI").GetComponent<Camera>().enabled = true;
        }
    }

    private void MarioDies() {

        // Mario Dies
        if (state.hearts <= 0) {
            AudioPlayer.playSoundFX("MarioPain");

            this.gameObject.transform.position = levelStartPivot.transform.position;

            rigidbodyMario.velocity = Vector2.zero;
            rigidbodyMario.angularVelocity = 0;

            state.hearts = 10;
            healthBar.value = state.hearts;

            animatorMario.SetBool("MarioHasShell", false);
            marioHasShell = false;
        }
    }
}