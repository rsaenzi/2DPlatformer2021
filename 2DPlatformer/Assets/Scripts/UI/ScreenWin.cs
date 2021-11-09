using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWin : MonoBehaviour {

    void Start() {
        Invoke("playMusicUINavigation", 6);
    }

    void playMusicUINavigation() {
        AudioPlayer.playMusic("UINavigation");
    }
}