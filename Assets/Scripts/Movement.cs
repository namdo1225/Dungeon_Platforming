using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    private float maxVelocity;
    private float velocity;
    private float acceleration;
    public const float rotationSpeed = 180f;
    private Rigidbody rigid;
    private Animator animator;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cam = Camera.main;
        maxVelocity = 10.0f;
        acceleration = 20.0f;
        velocity = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //controller.Move(move * 10.0f * Time.deltaTime);
        if (Input.GetKey(KeyCode.W))
        {
            // changing directions
            if (velocity < 0)
                velocity = 0;
            velocity += acceleration * Time.deltaTime;
            animator.SetFloat("Velocity", 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // changing directions
            if (velocity > 0)
                velocity = 0;
            velocity -= acceleration * Time.deltaTime;
            animator.SetFloat("Velocity", 1);
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            velocity = 0;
            animator.SetFloat("Velocity", 0);
        }
        // input rotation
        if (Input.GetKey(KeyCode.A))
        {
            controller.transform.Rotate(Vector3.up, -180 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            controller.transform.Rotate(Vector3.up, 180 * Time.deltaTime);
        }
        // cap velocity
        if (velocity > maxVelocity)
        {
            velocity = maxVelocity;
        }
        if (velocity < -maxVelocity)
        {
            velocity = -maxVelocity;
        }
        // get rotation
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        direction.y = 0.0f;
        // do move
        controller.Move(direction * velocity * Time.deltaTime);
    }
}
