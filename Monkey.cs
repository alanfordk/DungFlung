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
    public float generatePoopTime = .1f;
    public List<Poop> poops;
    public Vector2 _initialPosition;


    Vector2 DragStartPos;
    void Awake()
    {
        gameObject.tag = "Monkey";
        gameObject.layer = 7;
        poop = GameObject.FindGameObjectWithTag("poop").GetComponent<Poop>();
        poops.Add(poop);
        _initialPosition = poop.transform.position;
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
        if(poops.Count > 0)
        {

            poops[poops.Count-1].GetComponent<SpriteRenderer>().color = Color.red;
            poops[poops.Count - 1].poopLineRen.enabled = true;

            DragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
    }

    private void OnMouseUp()
    {
        if(poops.Count > 0)
        {

            poops[poops.Count - 1].GetComponent<SpriteRenderer>().color = Color.white;
            poops[poops.Count - 1].poopRB2D.gravityScale = 1;
            poops[poops.Count - 1].poopLineRen.enabled = false;

            Vector2 DragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = (DragEndPosition - DragStartPos) * poops[poops.Count-1]._launchPower;
            if (!poops[poops.Count - 1]._poopWasLaunched)
            {
                Debug.Log(_velocity);
                poops[poops.Count - 1].poopRB2D.velocity = -_velocity;
            }

            poops[poops.Count - 1]._poopWasLaunched = true;
            StartCoroutine(generatePoop());

            //audioManager.Play("MonkeyThrow");
        }



    }

    private void OnMouseDrag()
    {
        if(poops.Count > 0)
        {
            Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 DragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = (poops[poops.Count - 1]._initialPosition - newPosition) * poops[poops.Count - 1]._launchPower;
            Vector2[] trajectory = poops[poops.Count - 1].Plot(poops[poops.Count - 1].poopRB2D, (Vector2)transform.position, _velocity, 400);

            poops[poops.Count - 1].poopLineRen.positionCount = trajectory.Length;

            Vector3[] positions = new Vector3[trajectory.Length];
            for (int i = 0; i < trajectory.Length; i++)
            {
                positions[i] = trajectory[i];
            }
            poops[poops.Count - 1].poopLineRen.SetPositions(positions);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "poop" || collision.gameObject.tag == "Zombie" || collision.gameObject.tag == "smallZombie")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<PolygonCollider2D>(), GetComponent<CircleCollider2D>());
        }
    }

    IEnumerator generatePoop()
    {

        yield return new WaitForSeconds(generatePoopTime);
        createPoop();
    }

    public void createPoop()
    {
        Poop a = Instantiate(poops[poops.Count -1]);
        a.resetPoop();
        a.movePosition(_initialPosition);
        poops.RemoveAt(poops.Count - 1);
        poops.Add(a);

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
