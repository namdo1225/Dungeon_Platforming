/**
 * Description: Script to control player's dash (Shift key).
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Dash : MonoBehaviour
{
    public CharacterController controller;
    public Jump jump;
    public int max_dashes;
    private int cur_dashes;
    private float dash_velocity;
    private Vector3 dash_direction;
    private Camera cam;
    private float initial_dash = 15.0f;

    private Animator animator;

    [SerializeField]
    private AudioClip dashSFX;

    private AudioSource audSrc;

    [SerializeField]
    private InputActionReference dashControl;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = this.GetComponent<CharacterController>();
        audSrc = this.GetComponent<AudioSource>();

        cam = Camera.main;
        cur_dashes = 0;
        dash_direction = new Vector3(0, 0, 0);
        dash_velocity = 0.0f;
    }

    // Method to start a dash, including playing an animation and sfx.
    void StartDash()
    {
        // if player has not reached the maximum amount of dashes, then go ahead and start dash.
        if (cur_dashes < max_dashes)
        {
            animator.SetBool("Dash", true);
            audSrc.PlayOneShot(dashSFX);

            dash_direction = transform.TransformDirection(Vector3.forward);
            dash_direction.y = 0.3f;
            dash_velocity = initial_dash;
            cur_dashes++;
            jump.cancelJump();
        }
    }

    // Method to cancel a dash.
    public void cancelDash()
    {
        dash_velocity = 0.0f;
        animator.SetBool("Dash", false);
    }

    // Check if the player is dashing.
    public bool isDashing()
    {
        return dash_velocity > 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (dashControl.action.triggered)
        {
            StartDash();
        }
        dash_velocity -= initial_dash * Time.deltaTime;
        if (dash_velocity < 0.0f)
        {
            dash_velocity = 0.0f;
        }
        else
        {
            controller.Move(dash_direction * dash_velocity * Time.deltaTime);
        }
        if (jump.isGrounded)
        {
            animator.SetBool("Dash", false);
            cur_dashes = 0;
        }
    }

    // Method to enable getting certain inputs from player
    private void OnEnable()
    {
        dashControl.action.Enable();
    }

    // Method to disable getting certain inputs from player
    private void OnDisable()
    {
        dashControl.action.Disable();
    }
}
