using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class zombie : MonoBehaviour
{
    public Monkey monkey;
    public AudioManager audioManager;
    public PolygonCollider2D polygonColl;
    public bool zombieIsDead;
    public Poop poop;
    public int lifes;

    private Rigidbody2D zombieRB2D;

    private Vector3 _initialPosition;
    
    [SerializeField] private float _zombieSpeed = 1.5f;

    private void Awake()
    {
        zombieRB2D = GetComponent<Rigidbody2D>();
        zombieIsDead = false;
        monkey = GameObject.FindGameObjectWithTag("Monkey").GetComponent<Monkey>();
        //audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        lifes = 2;
    }
    private void Start()
    {
        _initialPosition = transform.position;
        zombieRB2D.velocity = new Vector2(-_zombieSpeed, 0);
        gameObject.tag = "Zombie";
    }

    private void Update()
    {
        if (transform.position.x < -9.5)
        {
            monkey.lifeLost();
            GameObject.Destroy(gameObject);
        }
        if (Math.Abs(zombieRB2D.velocity.x) < _zombieSpeed && !zombieIsDead)
        {
            zombieRB2D.AddForce(new Vector2(-0.2f, 0));
        }
        if (Math.Abs(zombieRB2D.velocity.x) > _zombieSpeed && !zombieIsDead)
        {
            zombieRB2D.AddForce(new Vector2(0.05f, 0));
        }
        //Debug.Log("real velocity: " + zombieRB2D.velocity.x + "\n" + "want velocity: " + _zombieSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "poop" && !zombieIsDead)
        {
            poopHitZombie();
        }
    }

    private void poopHitZombie()
    {
        lifes--;
        _zombieSpeed -= .4f;
        if (lifes == 0)
        {
            GetComponent<Animator>().SetBool("zombieHit", true);
            //audioManager.Play("ZombieDeath");
            monkey.updateScore();
            monkey.updateScore();
            zombieIsDead = true;
        }
        
    }
}
