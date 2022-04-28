using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monkey : MonoBehaviour
{
    public int score;
    public int lifes;
    public TextMeshProUGUI scoreGUI;
    public Poop poop;

    Vector2 DragStartPos;
    void Awake()
    {
        gameObject.tag = "Monkey";
        poop = GameObject.FindGameObjectWithTag("Poop").GetComponent<Poop>();
    }

    void Start()
    {
        score = 0;
        lifes = 5;
    }

    void Update()
    {
        scoreGUI.text = score.ToString();
    }

    private void OnMouseDown()
    {
        poop.GetComponent<SpriteRenderer>().color = Color.red;
        poop.poopLineRen.enabled = true;

        DragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

        poop.poopRB2D.gravityScale = 1;
        poop.poopLineRen.enabled = false;

        Vector2 DragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 _velocity = (DragEndPosition - DragStartPos) * poop._launchPower;
        if (!poop._poopWasLaunched)
        {
            poop.poopRB2D.velocity = -_velocity;
        }
        poop._poopWasLaunched = true;
        //audioManager.Play("MonkeyThrow");
    }

    private void OnMouseDrag()
    {
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 DragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 _velocity = (poop._initialPosition - newPosition) * poop._launchPower;
        Vector2[] trajectory = poop.Plot(poop.poopRB2D, (Vector2)transform.position, _velocity, 400);

        poop.poopLineRen.positionCount = trajectory.Length;

        Vector3[] positions = new Vector3[trajectory.Length];
        for (int i = 0; i < trajectory.Length; i++)
        {
            positions[i] = trajectory[i];
        }
        poop.poopLineRen.SetPositions(positions);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Poop" || collision.gameObject.tag == "Zombie" || collision.gameObject.tag == "smallZombie")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<PolygonCollider2D>(), GetComponent<PolygonCollider2D>());
        }
    }



    public void updateScore()
    {
        score++;
    }

    public void lifeLost()
    {
        lifes--;
        GetComponent<Health>().looseHealth();

    }


}
