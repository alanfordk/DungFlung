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

    [SerializeField] public float _launchPower = 1f;
    

    private void Awake()
    {
        poopLineRen = GetComponent<LineRenderer>();
        poopRB2D = GetComponent<Rigidbody2D>();
        //audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        _initialPosition = transform.position;
        gameObject.tag = "Poop";
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
            transform.position.x < -10)
        {
            resetPoop();
        }
        if (_timeSinceLaunched > .3)
        {
            resetPoop();
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
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<PolygonCollider2D>(), GetComponent<PolygonCollider2D>());
        }

    }

}
