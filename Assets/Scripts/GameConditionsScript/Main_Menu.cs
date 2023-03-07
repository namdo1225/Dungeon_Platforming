/**
 * Description: Load game main menu.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_Menu : MonoBehaviour
{

    // Method to load the main game menu (scene 0).
    public void GameScene()
    {
        SceneManager.LoadScene(0);
    }
}
