using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Win_Condition : MonoBehaviour
{
    //public MovementInput movement;
        void OnTriggerEnter(Collider other)
    {
        //movement.enabled = false;
        SceneManager.LoadScene(3);
    }
}
