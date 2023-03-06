using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Jump : MonoBehaviour
{
    public CharacterController controller;
    public Dash dash;
    public int max_jumps;
    private int jump_count;
    public bool isGrounded;
    public bool buffer_jump;
    public float buffer_timer;
    private float velocity;
    private const float acceleration = 25f;
    private float initial_jump = 15.0f;

    private Animator animator;

    private bool alreadyLanded = true;

    [SerializeField]
    private AudioClip jumpSFX;
    [SerializeField]
    private AudioClip landSFX;

    private AudioSource audSrc;

    [SerializeField]
    private InputActionReference jumpControl;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = this.GetComponent<CharacterController>();
        audSrc = this.GetComponent<AudioSource>();

        velocity = 0.0f;
        jump_count = 0;
        buffer_jump = false;
        buffer_timer = 0.0f;
        isGrounded = false;
    }

    bool canJump()
    {
        return isGrounded || jump_count < max_jumps;
    }

    public void cancelJump()
    {
        velocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded)
        {
            jump_count = 0;
        }
        if (jumpControl.action.triggered)
        {
            buffer_jump = true;
        }
        if (buffer_jump)
        {
            buffer_timer += Time.deltaTime;
            if (buffer_timer > 0.5f)
            {
                buffer_jump = false;
                buffer_timer = 0.0f;
            }
        }
        // start new jump
        if (buffer_jump && canJump())
        {
            alreadyLanded = false;
            animator.SetBool("Jump", true);
            audSrc.PlayOneShot(jumpSFX);
            if (dash.isDashing())
            {
                dash.cancelDash();
                // TODO: add super dash here!
            }
            velocity = initial_jump;
            jump_count++;
            buffer_jump = false;
        }
        // fall down
        if (!isGrounded)
        {
            velocity -= acceleration * Time.deltaTime;
            if (velocity < -initial_jump)
            {
                velocity = -initial_jump;
            }
        }
        // on the ground, not jumping
        if (velocity <= 0.0f && isGrounded)
        {
            if (!alreadyLanded)
            {
                animator.SetBool("Jump", false);
                audSrc.PlayOneShot(landSFX);
                alreadyLanded = true;
            }

            velocity = 0.0f;
            jump_count = 0;
        }
        Vector3 move = new Vector3();
        move.y = velocity * Time.deltaTime;
        controller.Move(move);
        isGrounded = controller.isGrounded;
    }

    private void OnEnable()
    {
        jumpControl.action.Enable();
    }

    private void OnDisable()
    {
        jumpControl.action.Disable();
    }
}
