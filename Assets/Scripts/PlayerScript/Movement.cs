/**
 * Description: Script to control player's translation and rotation (based on camera).
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public CharacterController controller;
    private const float rotationSpeed = 4f;
    private Rigidbody rigid;
    private Animator animator;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private InputActionReference movControl;

    private Transform camTransform;

    [SerializeField]
    private AudioClip stepSFX;
    private AudioSource audSrc;

    private float footStepInterval = 0.5f;
    private float footStepDelta = 0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audSrc = this.GetComponent<AudioSource>();
        camTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // move and/or rotate player (based on camera)
        Vector2 mov = movControl.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(mov.x, 0, mov.y);
        move = camTransform.forward * move.z + camTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        // control player's gravity and y down translation
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // if movement vector is not 0, then move/rotate the player.
        if (mov != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(mov.x, mov.y) * Mathf.Rad2Deg + camTransform.eulerAngles.y;
            Quaternion rot = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotationSpeed);
            playAudClip();
            animator.SetFloat("Velocity", 1);
        } else
        {
            animator.SetFloat("Velocity", 0);
        }

        // increase time passed since the last footstep audio clip played.
        footStepDelta += Time.deltaTime;
    }

    // Method to play the footstep audio clip.
    private void playAudClip()
    {
        if (footStepDelta > footStepInterval)
        {
            footStepDelta = 0;
            audSrc.PlayOneShot(stepSFX);
        }
    }

    // Method to enable getting certain inputs from player
    private void OnEnable()
    {
        movControl.action.Enable();
    }

    // Method to disable getting certain inputs from player
    private void OnDisable()
    {
        movControl.action.Disable();
    }
}
