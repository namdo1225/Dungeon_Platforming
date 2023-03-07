/**
 * Description: Class to control the lifetime of Zombie enemies.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField]
    private Rigidbody body;

    [SerializeField]
    public float moveSpeed = 1f;

    [SerializeField]
    public Animator animator;

    [SerializeField]
    public float deathTime = 45f;

    [SerializeField]
    [Range(0.25f,2f)]
    public float unpredicatbilityInterval = 0.5f;
    
    private int switcher;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SwitchBool",0,unpredicatbilityInterval);

        if (deathTime != 9999f)
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

        if (transform.position.y < -60f)
            Destroy(this.gameObject);

    }

    // Randomized setting values of a boolean variable.
    void SwitchBool()
    {
        switcher = Random.Range(0,2);
    }
}
