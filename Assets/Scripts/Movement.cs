using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            Vector3 tmpVec = new Vector3(0, 0, 1);
            tmpVec = transform.forward * speed * Time.deltaTime;
            rigid.AddRelativeForce(0, 0, speed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            Vector3 tmpVec = new Vector3(0, 0, 1);
            tmpVec = transform.forward * speed * Time.deltaTime;
            rigid.AddRelativeForce(0, 0, -speed * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.A))
        {
            Vector3 tmpVec = new Vector3(1, 0, 0);
            tmpVec = transform.forward * speed * Time.deltaTime;
            rigid.AddRelativeForce(-speed * Time.deltaTime, 0, 0);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            Vector3 tmpVec = new Vector3(1, 0, 0);
            tmpVec = transform.forward * speed * Time.deltaTime;
            rigid.AddRelativeForce(speed * Time.deltaTime, 0, 0);
        }
    }
}
