using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public CharacterController controller;
    public float hurt_interval = 1.5f;
    public int max_health = 3;
    public float knockback = 10.0f;
    private float cur_time;
    private int health;
    private GameObject checkpoint;
    private bool touching_enemy;
    private Vector3 knockback_direction;
    private float knockback_speed;
    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        cur_time = hurt_interval;
        health = max_health;
        checkpoint = null;
        knockback_direction = new Vector3(0, 0, 0);
        knockback_speed = 0.0f;
        // get default checkpoint
        var checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        if (checkpoints.Length > 0)
        {
            checkpoint = checkpoints[0];
        }
    }
    public int getHealth()
    {
        return health;
    }
    private void Update()
    {
        if (touching_enemy)
        {
            if (cur_time == hurt_interval)
            {
                health--;
                Debug.Log("Ouch! Health: " + health);
                if (health == 0)
                {
                    Debug.Log("Respawn");
                    Respawn();
                    health = max_health;
                    knockback_speed = 0.0f;
                }
            }
            if (cur_time > 0.0f)
            {
                cur_time -= Time.deltaTime;
            }
            else if (cur_time < 0.0f)
            {
                cur_time = hurt_interval;
            }
        }
        else
        {
            cur_time = hurt_interval;
        }
        if (knockback_speed > 0.0f)
        {
            controller.Move(knockback_direction * knockback_speed * Time.deltaTime);
            knockback_speed -= knockback * Time.deltaTime;
        }
    }

    private void Respawn()
    {
        if (checkpoint != null)
        {
            Vector3 new_pos = checkpoint.GetComponent<Renderer>().bounds.center;
            controller.Move(new_pos - transform.position);
        }
        else
        {
            controller.Move(new Vector3(0.0f, 0.0f, 0.0f) - transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        var gameObject = collision.gameObject;
        if (gameObject.tag == "Enemy")
        {
            touching_enemy = true;
            knockback_speed = knockback;
            knockback_direction = transform.position - gameObject.GetComponent<Renderer>().bounds.center;
            knockback_direction.Normalize();
        }
        else if (gameObject.tag == "Checkpoint")
        {
            Debug.Log("Checkpoint");
            checkpoint = gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        var gameObject = collision.gameObject;
        if (gameObject.tag == "Enemy")
        {
            touching_enemy = false;
        }
    }
}
