using UnityEngine;
using UnityEngine.SceneManagement;

public class Poop : MonoBehaviour
{
    public Vector2 _initialPosition;
    public GameObject poopPrefab;


    public bool _poopWasLaunched;
    private float _timeSinceLaunched;
    public Rigidbody2D poopRB2D;
    public LineRenderer poopLineRen;

    public AudioManager audioManager;

    public float _launchPower = 3f;


    private void Awake()
    {
        poopLineRen = GetComponent<LineRenderer>();
        poopRB2D = GetComponent<Rigidbody2D>();
        //audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        _initialPosition = transform.position;
        gameObject.tag = "poop";
        gameObject.layer = 6;
        Physics2D.IgnoreLayerCollision(6, 6, true);
        Physics2D.IgnoreLayerCollision(6, 7, true);
    }

    private void Update()
    {
        if (_poopWasLaunched && poopRB2D.velocity.magnitude < .1)
        {
            _timeSinceLaunched += Time.deltaTime;
        }

        if (transform.position.y > 10 ||
            transform.position.y < -10 ||
            transform.position.x > 10 ||
            transform.position.x < -10 ||
            _timeSinceLaunched > .3)
        {
            destroyPoop();
        }

    }


    public void resetPoop()
    {
        transform.position = _initialPosition;
        transform.rotation = Quaternion.identity;
        poopRB2D.angularVelocity = 0;
        poopRB2D.velocity = Vector3.zero;
        poopRB2D.gravityScale = 0;
        _poopWasLaunched = false;
        _timeSinceLaunched = 0;

    }

    public void movePosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * 1 * timestep * timestep;

        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = velocity * timestep;

        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }
        return results;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monkey")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<CircleCollider2D>(), GetComponent<PolygonCollider2D>());
        }

        if(collision.gameObject.tag == "poop")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<PolygonCollider2D>(), GetComponent<PolygonCollider2D>());
            Debug.Log("poop colision");
        }

        if (collision.gameObject.tag == "Zombie")
        {
            Destroy(gameObject);
        }



    }

    public void destroyPoop()
    {
        Destroy(gameObject);
    }

}
