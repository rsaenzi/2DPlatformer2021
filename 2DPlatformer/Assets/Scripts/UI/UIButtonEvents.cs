using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonEvents : MonoBehaviour {

    // ScreenMenu

    public void ScreenMenuButtonPlay() {
        AudioPlayer.playSoundFX("Click");
        UINavigation.changeScreen("ScreenMenu", "ScreenLevels");
    }


    // ScreenLevels

    public void ScreenLevelsButtonLevel1() {
        AudioPlayer.playSoundFX("Click");
        AudioPlayer.stopAllMusic();
        AudioPlayer.playMusic("Level1Grass");

        UINavigation.changeScreen("ScreenLevels", "ScreenHUD");
        LevelLoader.loadLevel("Level1");
        GameObject.Find("CameraUI").GetComponent<Camera>().enabled = false;
    }

    public void ScreenLevelsButtonLevel2() {
        AudioPlayer.playSoundFX("Click");
        AudioPlayer.stopAllMusic();
        AudioPlayer.playMusic("Level2Sand");

        UINavigation.changeScreen("ScreenLevels", "ScreenHUD");
        LevelLoader.loadLevel("Level2");
        GameObject.Find("CameraUI").GetComponent<Camera>().enabled = false;
    }

    public void ScreenLevelsButtonLevel3() {
        AudioPlayer.playSoundFX("Click");
        AudioPlayer.stopAllMusic();
        AudioPlayer.playMusic("Level3Lava");

        UINavigation.changeScreen("ScreenLevels", "ScreenHUD");
        LevelLoader.loadLevel("Level3");
        GameObject.Find("CameraUI").GetComponent<Camera>().enabled = false;
    }

    public void ScreenLevelsButtonBack() {
        AudioPlayer.playSoundFX("Click");
        UINavigation.changeScreen("ScreenLevels", "ScreenMenu");
    }


    // ScreenPause

    public void ScreenPauseButtonResume() {
        AudioPlayer.playSoundFX("Click");
        UINavigation.removeScreen("ScreenPause");
        Time.timeScale = 1;
    }


    // ScreenVideo

    public void ScreenVideoButtonSkip() {
        AudioPlayer.playSoundFX("Click");
        AudioPlayer.playMusic("UINavigation");
        UINavigation.changeScreen("ScreenVideo", "ScreenMenu");
    }


    // ScreenWin

    public void ScreenWinButtonMenu() {
        AudioPlayer.playSoundFX("Click");
        UINavigation.changeScreen("ScreenWin", "ScreenMenu");
    }

    public void ScreenWinButtonLevels() {
        AudioPlayer.playSoundFX("Click");
        UINavigation.changeScreen("ScreenWin", "ScreenLevels");
    }


    // ScreenHUD

    public void ScreenHudButtonPause() {
        AudioPlayer.playSoundFX("Click");
        UINavigation.showScreen("ScreenPause");
        Time.timeScale = 0;
    }
}