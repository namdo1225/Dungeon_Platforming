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
    private GameObject checkpoint = null;
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

    [SerializeField]
    private AudioClip respawnSFX;
    [SerializeField]
    private AudioClip deathSFX;
    [SerializeField]
    private AudioClip hitSFX;

    private AudioSource audSrc;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        audSrc = this.GetComponent<AudioSource>();

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
            changeCheckParticle(true);

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
                playAudClip(2);
                health--;
                changeHealthImg();
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
        controller.enabled = false;
        health = max_health;
        changeHealthImg();
        playAudClip(1);
        knockback_speed = 0.0f;
        if (checkpoint != null)
        {
            Vector3 new_pos = checkpoint.GetComponent<BoxCollider>().transform.position + new Vector3(0, 1, 0);
            transform.position = new_pos;
        }
        else
        {
            transform.position = Vector3.zero;
        }
        playAudClip(0);
        controller.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var gameObject = collision.gameObject;
        if (gameObject.tag == "Enemy")
        {
            touching_enemy = true;
            knockback_speed = knockback;
            knockback_direction = transform.position - gameObject.GetComponent<Rigidbody>().transform.position;
            knockback_direction.Normalize();
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

    private void OnTriggerEnter(Collider col)
    {
        var gameObject = col.gameObject;
        if (gameObject.tag == "Checkpoint" && checkpoint != gameObject)
        {
            if (checkpoint != null)
                changeCheckParticle(false);

            checkpoint = gameObject;
            changeCheckParticle(true);

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

    private void playAudClip(int type)
    {
        if (type == 0)
            audSrc.PlayOneShot(respawnSFX);
        else if (type == 1)
            audSrc.PlayOneShot(deathSFX);
        else if (type == 2)
            audSrc.PlayOneShot(hitSFX);
    }

    private void changeCheckParticle(bool isTouched)
    {
        // set passive particle system on checkpoint to (in)active
        checkpoint.transform.GetChild(1).gameObject.SetActive(!isTouched);

        // set active checkpoint particle system on checkpoint to (in)active
        checkpoint.transform.GetChild(2).gameObject.SetActive(isTouched);
    }
}