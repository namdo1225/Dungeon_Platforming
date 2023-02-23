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
    }

    void FixedUpdate()
    {
        Vector3 input = new Vector3(0, Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (rigid.velocity.magnitude > maxSpeed)
        {
            rigid.velocity = rigid.velocity.normalized * maxSpeed;
        }

        if (input.magnitude > 0.001)
        {
            rigid.AddRelativeForce(new Vector3(0, 0, input.z * speed * Time.deltaTime));
            rigid.AddRelativeTorque(new Vector3(0, rotationSpeed * input.y * Time.deltaTime, 0));
        }
    }
}
