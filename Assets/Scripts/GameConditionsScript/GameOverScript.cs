/**
 * Description: Load game over scene if player enters certain defined objects.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

    // Check if player has triggered the game over event.
    void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.tag == "Player")
            SceneManager.LoadScene(2);
    }
}
