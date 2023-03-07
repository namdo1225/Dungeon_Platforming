/**
 * Description: Script to turn on/off tutorial HUD.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorialMsg;

    // Method to turn on/off the tutorial.
    public void TurnOnOff()
    {
        tutorialMsg.gameObject.SetActive(!tutorialMsg.gameObject.activeSelf);
    }
}
