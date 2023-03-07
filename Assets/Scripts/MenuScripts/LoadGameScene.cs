/**
 * Description: Script to load the game world.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameScene : MonoBehaviour
{

    // Method to load the game world.
    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameWorld");
    }
}
