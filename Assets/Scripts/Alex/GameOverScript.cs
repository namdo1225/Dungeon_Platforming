using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Game Scene");
        SceneManager.LoadScene(2);
    }
}
