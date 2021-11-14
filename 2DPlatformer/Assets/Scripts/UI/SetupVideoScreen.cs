using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SetupVideoScreen : MonoBehaviour {

    void Start() {
        this.gameObject.GetComponent<VideoPlayer>().targetCamera = GameObject.Find("CameraUI").GetComponent<Camera>();
        this.gameObject.GetComponent<VideoPlayer>().loopPointReached += VideoFinished;
    }

    void VideoFinished(VideoPlayer player) {
        UINavigation.changeScreen("ScreenVideo", "ScreenMenu");
        AudioPlayer.playMusic("UINavigation");
    }
}