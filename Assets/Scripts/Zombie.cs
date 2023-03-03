using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Rigidbody body;
    public float moveSpeed;
    public Animator animator;
    public float deathTime = 45f;
    [Range(0.25f,2f)]
    public float unpredicatbilityInterval = 0.5f;
    int switcher;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SwitchBool",0,unpredicatbilityInterval);
        Destroy(this.gameObject, deathTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (switcher == 1)
        {
            animator.SetBool("Attacking",true);
        }
        
        else
        {
            animator.SetBool("Attacking",false);
        }
        body.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
    }

    // void OnCollisionEnter(Collision col)
    // {
    //     if (col.GetComponent<IsPlayer>())
    //     {
    //         Destroy(this.gameObject);
    //     }
    // }

    void SwitchBool()
    {
        switcher = Random.Range(0,2);
        //Debug.Log(switcher);
    }
}
