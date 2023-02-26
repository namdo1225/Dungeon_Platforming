using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        cam = Camera.main;
        cur_dashes = 0;
        dash_direction = new Vector3(0, 0, 0);
        dash_velocity = 0.0f;
    }

    void StartDash()
    {
        if (cur_dashes < max_dashes)
        {
            dash_direction = transform.TransformDirection(Vector3.forward);
            dash_direction.y = 0.3f;
            dash_velocity = 50.0f;
            cur_dashes++;
            jump.cancelJump();
        }
    }

    public void cancelDash()
    {
        dash_velocity = 0.0f;
    }

    public bool isDashing()
    {
        return dash_velocity > 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartDash();
        }
        dash_velocity -= 50.0f * Time.deltaTime;
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
            cur_dashes = 0;
        }
    }
}
