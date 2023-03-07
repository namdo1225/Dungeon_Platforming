/**
 * Description: Class to handle the movement of barrier objects.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierMover : MonoBehaviour
{
    private float xPos;
    private Vector3 myPos;

    [SerializeField]
    private float movementAmount=1;

    [SerializeField]
    private float movementSpeed = 1;

    private Transform barrierPos;

    // Start is called before the first frame update
    void Start()
    {
        barrierPos = GetComponent<Transform>();
        myPos = barrierPos.position;
    }

    // Update is called once per frame. Function makes sure barrier move back and forth around a certain spot.
    void Update()
    {
        xPos= Mathf.Sin(Time.time * movementSpeed);
        barrierPos.position = myPos + new Vector3(xPos*movementAmount,0,0);
    }
}
