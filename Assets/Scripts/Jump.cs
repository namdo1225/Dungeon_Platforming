using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
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
        if (Input.GetKeyDown(KeyCode.Space))
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
            velocity = 0.0f;
            jump_count = 0;
        }
        Vector3 move = new Vector3();
        move.y = velocity * Time.deltaTime;
        controller.Move(move);
        isGrounded = controller.isGrounded;
    }
}
