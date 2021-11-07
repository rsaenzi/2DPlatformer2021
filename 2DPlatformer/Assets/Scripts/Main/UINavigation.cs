//
//  UINavigation.cs
//
//  Created by Rigoberto Sáenz Imbacuán (https://linkedin.com/in/rsaenzi/)
//  Copyright © 2021. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 
 * // How to use this script:
 * UINavigation.showScreen("ScreenName");
 * UINavigation.removeScreen("ScreenName");
 * UINavigation.removeAllScreens();
 * UINavigation.changeScreen("ScreenToRemove", "ScreenToShow");
 * 
 */

public class UINavigation {

    // Relative path of UI screens inside the Resources folder
    private static string pathInsideResources = "UI";

    // Name of canvas object that will host the UI screens
    private static string nameCanvasContainer = "Canvas";


    /// <summary>
    /// This function creates a new prefab instance (copy) of a UI screen from Resources folder.
    /// Each UI screen is just a Panel that contains all the UI elements for that screen. The panel is set as child of Canvas object.
    /// </summary>
    /// <param name="nameScreenToShow">Name of the prefab from Resources folder to load inside Canvas</param>
    public static void showScreen(string nameScreenToShow) {

        // Find the canvas object
        GameObject canvasObject = GameObject.Find(nameCanvasContainer);

        if (canvasObject == null) {
            Debug.LogError("UINavigation.showScreen(): GameObject '" + nameCanvasContainer + "' could not be found");
            Debug.Break();
            return;
        }

        // Loads the prefab as a resource
        string pathScreenToShow = pathInsideResources + "/" + nameScreenToShow;
        Object prefabScreenToShow = Resources.Load(pathScreenToShow);

        // Check if the prefab could be found
        if (prefabScreenToShow == null) {
            Debug.LogError("UINavigation.showScreen(): Level prefab 'Resources/" + pathScreenToShow + "' could not be found");
            Debug.Break();
            return;
        }

        // Creates an instance of the prefab object
        GameObject screenToShow = GameObject.Instantiate(prefabScreenToShow as GameObject);

        // Make the new screen child of canvas object
        screenToShow.transform.SetParent(canvasObject.transform);

        // Set the correct screen name
        screenToShow.name = nameScreenToShow;
        screenToShow.SetActive(true);

        // Reset new screen's transform
        screenToShow.transform.rotation = Quaternion.identity;
        screenToShow.transform.position = Vector3.zero;
        screenToShow.transform.localScale = Vector3.one;

        // Locate anchors on screen's corners
        RectTransform rectComponent = screenToShow.GetComponent<RectTransform>();

        if (rectComponent == null) {
            Debug.LogError("UINavigation.showScreen(): Component 'RectTransform' could not be found");
            Debug.Break();
            return;
        }

        rectComponent.anchorMin = Vector2.zero;
        rectComponent.anchorMax = Vector2.one;

        // Move vertices to anchor's position
        rectComponent.offsetMin = Vector2.zero;
        rectComponent.offsetMax = Vector2.zero;
    }


    /// <summary>
    /// This function destroys a UI screen inside the Canvas object.
    /// Each UI screen is just a Panel that contains all the UI elements for that screen. The panel is set as child of Canvas object.
    /// </summary>
    /// <param name="nameScreenToRemove">Name of the gameObject inside Canvas to destroy</param>
    public static void removeScreen(string nameScreenToRemove) {

        // Find the canvas object
        GameObject canvasObject = GameObject.Find(nameCanvasContainer);

        if (canvasObject == null) {
            Debug.LogError("UINavigation.removeScreen(): GameObject '" + nameCanvasContainer + "' could not be found");
            Debug.Break();
            return;
        }

        // Find the game object to be deleted
        Transform transformScreenToRemove = canvasObject.transform.Find(nameScreenToRemove);

        if (transformScreenToRemove == null) {
            string pathScreenToRemove = nameCanvasContainer + "/" + nameScreenToRemove;
            Debug.LogError("UINavigation.removeScreen(): GameObject '" + pathScreenToRemove + "' could not be found");
            Debug.Break();
            return;
        }

        // Destroy the UI screen
        GameObject.Destroy(transformScreenToRemove.gameObject);
    }


    /// <summary>
    /// This function destroys all UI screens inside the Canvas object.
    /// Each UI screen is just a Panel that contains all the UI elements for that screen. The panel is set as child of Canvas object.
    /// </summary>
    public static void removeAllScreens() {

        // Find the canvas object
        GameObject canvasObject = GameObject.Find(nameCanvasContainer);

        if (canvasObject == null) {
            Debug.LogError("UINavigation.removeAllScreens(): GameObject '" + nameCanvasContainer + "' could not be found");
            Debug.Break();
            return;
        }

        // Destroy each UI screen
        foreach (Transform transformScreenToRemove in canvasObject.transform) {
            GameObject.Destroy(transformScreenToRemove.gameObject);
        }
    }


    /// <summary>
    /// This function destroys a UI screen inside the Canvas object, then creates a new prefab instance (copy) of a UI screen from Resources folder.
    /// Each UI screen is just a Panel that contains all the UI elements for that screen. The panel is set as child of Canvas object.
    /// </summary>
    /// <param name="nameScreenToRemove">Name of the gameObject inside Canvas to destroy</param>
    /// <param name="nameScreenToShow">Name of the prefab from Resources folder to load inside Canvas</param>
    public static void changeScreen(string nameScreenToRemove, string nameScreenToShow) {
        removeScreen(nameScreenToRemove);
        showScreen(nameScreenToShow);
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