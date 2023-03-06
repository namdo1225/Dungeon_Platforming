using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_Menu : MonoBehaviour
{
    public void GameScene()
    {
        Debug.Log("Main Menu Scene");
        SceneManager.LoadScene(0);
    }
}
