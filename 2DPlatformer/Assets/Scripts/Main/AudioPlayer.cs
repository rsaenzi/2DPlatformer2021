//
//  AudioPlayer.cs
//
//  Created by Rigoberto Sáenz Imbacuán (https://linkedin.com/in/rsaenzi/)
//  Copyright © 2021. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * // How to use this script:
 * AudioPlayer.playSoundFX("SoundName");
 * AudioPlayer.playMusic("MusicName");
 * AudioPlayer.stopMusic("MusicName");
 * AudioPlayer.stopAllSoundFXs();
 * AudioPlayer.stopAllMusic();
 * 
 */

public class AudioPlayer {

    // Name of parent game object containing all the sound effects
    static string nameSoundFxContainer = "SoundFx";

    // Name of parent game object containing all the music songs
    static string nameMusicContainer = "Music";


    /// <summary>
    /// This function plays the sound effect assigned to a game object inside the 'SoundFx' object.
    /// Each sound effect is a game object with an AudioSource attached to.
    /// </summary>
    /// <param name="nameSoundFxToPlay">Name of the sound effect to play</param>
    public static void playSoundFX(string nameSoundFxToPlay) {

        // Finds the audio source
        AudioSource soundFxToPlay = getAudioComponent(nameSoundFxContainer, nameSoundFxToPlay, "playSoundFX");

        if (soundFxToPlay == null) {
            Debug.Break();
            return;
        }

        // Plays the audio clip attached to the audio source component
        soundFxToPlay.playOnAwake = false;
        soundFxToPlay.loop = false;
        soundFxToPlay.Play();
    }


    /// <summary>
    /// This function plays the music audio assigned to a game object inside the 'Music' object.
    /// Each music audio is a game object with an AudioSource attached to.
    /// </summary>
    /// <param name="nameMusicToPlay">Name of the sound effect to play</param>
    public static void playMusic(string nameMusicToPlay) {

        // Finds the audio source
        AudioSource musicToPlay = getAudioComponent(nameMusicContainer, nameMusicToPlay, "playMusic");

        if (musicToPlay == null) {
            Debug.Break();
            return;
        }

        // Plays the audio clip attached to the audio source component
        musicToPlay.loop = true;
        musicToPlay.Play();
    }


    /// <summary>
    /// This function stops the music audio assigned to a game object inside the 'Music' object.
    /// Each music audio is a game object with an AudioSource attached to.
    /// </summary>
    /// <param name="nameMusicToStop">Name of the sound effect to play</param>
    public static void stopMusic(string nameMusicToStop) {

        // Finds the audio source
        AudioSource musicToStop = getAudioComponent(nameMusicContainer, nameMusicToStop, "stopMusic");

        if (musicToStop == null) {
            Debug.Break();
            return;
        }

        // Stops the audio clip attached to the audio source component
        musicToStop.Stop();
    }


    /// <summary>
    /// This function stops all the sound effect assigned to all game objects inside the 'SoundFx' object.
    /// Each sound effect is a game object with an AudioSource attached to.
    /// </summary>
    public static void stopAllSoundFXs() {
        stopAllAudios(nameSoundFxContainer, "stopAllSoundFXs");
    }


    /// <summary>
    /// This function stops all the music audios assigned to all game objects inside the 'Music' object.
    /// Each music audio is a game object with an AudioSource attached to.
    /// </summary>
    public static void stopAllMusic() {
        stopAllAudios(nameMusicContainer, "stopAllMusic");
    }


    private static AudioSource getAudioComponent(string nameContainer, string nameAudio, string nameFunction) {

        // Find the audio container
        GameObject audioContainer = GameObject.Find(nameContainer);

        if (audioContainer == null) {
            Debug.LogError("AudioPlayer." + nameFunction + "(): GameObject '" + nameContainer + "' could not be found");
            Debug.Break();
            return null;
        }

        // Find the game object that contains the required audio
        Transform transformAudioComponent = audioContainer.transform.Find(nameAudio);

        if (transformAudioComponent == null) {
            string pathAudio = nameContainer + "/" + nameAudio;
            Debug.LogError("AudioPlayer." + nameFunction + "(): GameObject '" + pathAudio + "' could not be found");
            Debug.Break();
            return null;
        }

        // Search for the audio source component
        AudioSource audioComponent = transformAudioComponent.gameObject.GetComponent<AudioSource>();

        // Check if the component could be found
        if (audioComponent == null) {
            Debug.LogError("AudioPlayer." + nameFunction + "(): Component 'AudioSource' in GameObject '" + transformAudioComponent.gameObject.name + "' could not be found");
            Debug.Break();
            return null;
        }

        return audioComponent;
    }

    private static void stopAllAudios(string nameContainer, string nameFunction) {

        // Find the audio container
        GameObject audioContainer = GameObject.Find(nameContainer);

        if (audioContainer == null) {
            Debug.LogError("AudioPlayer." + nameFunction + "(): GameObject '" + nameContainer + "' could not be found");
            Debug.Break();
            return;
        }

        // Stops each audio
        foreach (Transform transformAudioToStop in audioContainer.transform) {

            // Search for the audio source component
            AudioSource audioComponent = transformAudioToStop.gameObject.GetComponent<AudioSource>();

            // Check if the component could be found
            if (audioComponent == null) {
                Debug.LogError("AudioPlayer." + nameFunction + "(): Component 'AudioSource' in GameObject '" + transformAudioToStop.gameObject.name + "' could not be found");
                Debug.Break();
                return;
            }

            // Stops the audio clip attached to the audio source component
            audioComponent.Stop();
        }
    }
}

/*
 * 
 * Related:
 * 
 *   AudioPlayer: Script for Unity to play/stop sound fx and music
 *   https://gist.github.com/rsaenzi/119357d38d9c7255adfa8e465d486d0c
 * 
 *   UINavigation: Script for Unity to load/unload UI screens
 *   https://gist.github.com/rsaenzi/95df01c056d352651a9143b13ce305fa
 * 
 *   LevelLoader: Script for Unity to load/unload levels
 *   https://gist.github.com/rsaenzi/33cfff7d20e62c6f229442f7c6a7aa90
 * 
 */