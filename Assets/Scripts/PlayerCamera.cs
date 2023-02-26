using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    private float turnSpeed;
    void Start()
    {
        offset = transform.position - player.transform.position;
        turnSpeed = 10.0f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.TransformPoint(offset);
        transform.LookAt(player.transform);
    }
}
