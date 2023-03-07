/**
 * Description: Script to pause the game (and stop gameplay flow).
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private GameObject menus;
    private bool pause = false;

    [SerializeField]
    private InputActionReference escControl;

    // Start is called before the first frame update
    private void Start()
    {
        menus = GameObject.Find("PauseMenu");
        menus.SetActive(false);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (escControl.action.triggered)
        {
            pause = !pause;
            if (pause)
                Pause();
            else
                Resume();
        }
    }

    // Pause the game by setting time scale to 0.
    private void Pause()
    {
        menus.SetActive(true);
        Time.timeScale = 0;
    }

    // Resume the game by resetting time scale.
    public void Resume()
    {
        menus.SetActive(false);
        Time.timeScale = 1;
    }

    // Load the main menu.
    public void LoadMainMenu()
    {
        menus.SetActive(false);
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    // Quit game.
    public void Quit()
    {
        Application.Quit();
    }

    // Enable getting input from player
    private void OnEnable()
    {
        escControl.action.Enable();
    }

    // Disable getting input from player
    private void OnDisable()
    {
        escControl.action.Disable();
    }
}