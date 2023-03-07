/**
 * Description: Script to load the game scene once the player chooses to respawn.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Win_Condition : MonoBehaviour
{

    // Method to trigger the win scene once player has entered a defined collider.
    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(3);
    }
}