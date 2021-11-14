using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWin : MonoBehaviour {

    void Start() {
        Invoke("playUIMusic", 6.5f);
    }

    void playUIMusic() {
        AudioPlayer.playMusic("UINavigation");
    }
}