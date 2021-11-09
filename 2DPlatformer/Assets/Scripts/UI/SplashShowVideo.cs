using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashShowVideo : MonoBehaviour {

    void Start() {
        AudioPlayer.playSoundFX("Egg");
        Invoke("showScreenVideo", 3);
    }

    void showScreenVideo() {
        UINavigation.changeScreen("ScreenSplash", "ScreenVideo");
    }
}