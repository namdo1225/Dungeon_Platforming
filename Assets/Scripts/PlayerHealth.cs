using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private RawImage healthImg;
    [SerializeField]
    private Texture health3;
    [SerializeField]
    private Texture health2;
    [SerializeField]
    private Texture health1;

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
                changeHealthImg();
                Debug.Log("Ouch! Health: " + health);
                if (health == 0)
                    Respawn();
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

        if (transform.position.y < -60.0f)
            Respawn();
    }

    private void Respawn()
    {
        health = max_health;
        changeHealthImg();
        knockback_speed = 0.0f;
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

    private void changeHealthImg()
    {
        if (health == 3)
        {
            healthImg.texture = health3;
        } else if (health == 2)
        {
            healthImg.texture = health2;
        } else if (health == 1)
        {
            healthImg.texture = health1;
        }
    }
}
