//
//  LevelLoader.cs
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
 * LevelLoader.loadLevel("LevelWater");
 * LevelLoader.unloadLevel("LevelFire");
 * LevelLoader.unloadAllLevels();
 * 
 */

public class LevelLoader {

    // Relative path of level prefabs inside the Resources folder
    static string pathInSideResources = "Levels";

    // Name of parent game object containing all the levels
    static string nameLevelsContainer = "Levels";


    /// <summary>
    /// This function creates a new prefab instance (copy) of a level from Resources folder.
    /// Each level is just an empty game object that contains all the elements for that level.
    /// </summary>
    /// <param name="nameLevelToLoad">Name of the prefab from Resources folder to load</param>
    public static void loadLevel(string nameLevelToLoad) {

        // Find the levels object
        GameObject levelsObject = GameObject.Find(nameLevelsContainer);

        if (levelsObject == null) {
            Debug.LogError("UINavigation.loadLevel(): GameObject '" + nameLevelsContainer + "' could not be found");
            Debug.Break();
            return;
        }

        // Loads the prefab as a resource
        string pathLevelToLoad = pathInSideResources + "/" + nameLevelToLoad;
        Object prefabLevelToLoad = Resources.Load(pathLevelToLoad);

        // Check if the prefab could be found
        if (prefabLevelToLoad == null) {
            Debug.LogError("LevelLoader.loadLevel(): Level prefab 'Resources/" + pathLevelToLoad + "' could not be found");
            Debug.Break();
            return;
        }

        // Creates an instance of the prefab object
        GameObject levelToLoad = GameObject.Instantiate(prefabLevelToLoad as GameObject);

        // Make the new level child of levels object
        levelToLoad.transform.SetParent(levelsObject.transform);

        // Set the correct level name
        levelToLoad.name = nameLevelToLoad;
        levelToLoad.SetActive(true);

        // Reset the transform
        levelToLoad.transform.rotation = Quaternion.identity;
        levelToLoad.transform.position = Vector3.zero;
        levelToLoad.transform.localScale = Vector3.one;
    }


    /// <summary>
    /// This function destroys a level already loaded in the scene.
    /// Each level is just an empty game object that contains all the elements for that level.
    /// </summary>
    /// <param name="nameLevelToUnload">Name of the gameObject to destroy</param>
    public static void unloadLevel(string nameLevelToUnload) {

        // Find the levels object
        GameObject levelsObject = GameObject.Find(nameLevelsContainer);

        if (levelsObject == null) {
            Debug.LogError("UINavigation.unloadLevel(): GameObject '" + nameLevelsContainer + "' could not be found");
            Debug.Break();
            return;
        }

        // Find the game object to be deleted
        Transform transformLevelToUnload = levelsObject.transform.Find(nameLevelToUnload);

        if (transformLevelToUnload == null) {
            string pathLevelToUnload = nameLevelsContainer + "/" + nameLevelToUnload;
            Debug.LogError("UINavigation.unloadLevel(): GameObject '" + pathLevelToUnload + "' could not be found");
            Debug.Break();
            return;
        }

        // Destroy the level
        GameObject.Destroy(transformLevelToUnload.gameObject);
    }


    /// <summary>
    /// This function destroys all levels inside the Levels object.
    /// Each level is just an empty game object that contains all the elements for that level.
    /// </summary>
    public static void unloadAllLevels() {

        // Find the levels object
        GameObject levelsObject = GameObject.Find(nameLevelsContainer);

        if (levelsObject == null) {
            Debug.LogError("UINavigation.unloadAllLevels(): GameObject '" + nameLevelsContainer + "' could not be found");
            Debug.Break();
            return;
        }

        // Destroy each level
        foreach (Transform transformLevelToUnload in levelsObject.transform) {
            GameObject.Destroy(transformLevelToUnload.gameObject);
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