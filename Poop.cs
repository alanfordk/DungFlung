using UnityEngine;
using UnityEngine.SceneManagement;

public class Poop : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _poopWasLaunched;
    private float _timeSittingAround;
    private Rigidbody2D poopRB2D;
    private LineRenderer poopLineRen;

    public AudioManager audioManager;

    [SerializeField] private float _launchPower = 45;
    

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
        if (_poopWasLaunched && poopRB2D.velocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
        }

        if (transform.position.y > 10 || 
            transform.position.y < -10 ||
            transform.position.x > 10 ||
            transform.position.x < -10 ||
            _timeSittingAround > .2)
        {
            resetPoop();
        }

        //update line renderer
        poopLineRen.SetPosition(1, _initialPosition);
        poopLineRen.SetPosition(0, transform.position);
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        poopLineRen.enabled = true;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        poopRB2D.AddForce(directionToInitialPosition * _launchPower);
        poopRB2D.gravityScale = 1;
        _poopWasLaunched = true;
        poopLineRen.enabled = false;

        
        //audioManager.Play("MonkeyThrow");
    }


    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }

    public void resetPoop()
    {
        transform.position = _initialPosition;
        transform.rotation = Quaternion.identity;
        poopRB2D.velocity = Vector3.zero;
        poopRB2D.gravityScale = 0;
        _poopWasLaunched = false;
        _timeSittingAround = 0;
    }
}
