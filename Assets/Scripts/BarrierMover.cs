using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierMover : MonoBehaviour
{

    public Transform barrierPos;
    float xPos;
    Vector3 myPos;
    public float movementAmount=1;
    void Start()
    {
        myPos = barrierPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        xPos= Mathf.Sin(Time.time);
        barrierPos.position = myPos + new Vector3(xPos*movementAmount,0,0);
    }
}
