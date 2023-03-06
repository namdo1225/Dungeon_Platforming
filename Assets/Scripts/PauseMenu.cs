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

    private void Pause()
    {
        menus.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        menus.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        menus.SetActive(false);
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        escControl.action.Enable();
    }

    private void OnDisable()
    {
        escControl.action.Disable();
    }
}