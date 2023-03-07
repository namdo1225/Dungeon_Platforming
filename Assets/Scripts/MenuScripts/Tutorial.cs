using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    [SerializeField]
    private GameObject tutorialMsg;

    public void TurnOnOff()
    {
        tutorialMsg.gameObject.SetActive(!tutorialMsg.gameObject.activeSelf);
    }
}
