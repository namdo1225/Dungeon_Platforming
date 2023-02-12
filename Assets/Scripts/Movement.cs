using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private float maxSpeed;
    public float rotationSpeed;

    private Rigidbody rigid;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 10f;
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rigid.velocity.z != 0 || rigid.velocity.x != 0)
        {
            animator.SetFloat("Velocity", 0.2f);
        }
        else
        {
            animator.SetFloat("Velocity", 0f);
        }

        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            rigid.AddRelativeForce(0, 0, speed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            rigid.AddRelativeForce(0, 0, -speed * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.A))
        {
            rigid.AddRelativeTorque(0, -rotationSpeed * Time.deltaTime, 0);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rigid.AddRelativeTorque(0, rotationSpeed * Time.deltaTime, 0);
        }
    }

    void FixedUpdate()
    {
        if (rigid.velocity.magnitude > maxSpeed)
        {
            rigid.velocity = rigid.velocity.normalized * maxSpeed;
        }
    }
}
