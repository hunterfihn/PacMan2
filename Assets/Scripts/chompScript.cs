using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chompScript : MonoBehaviour
{
    Rigidbody rb;
    NavMeshAgent pinkGhostAgent;
    TextMesh theScoreTextMesh;
    TextMesh ghostModeTextMesh;


    public GameObject ScoreText;
    public GameObject ModeText;
    public float speed = 20.0f;
    public GameObject pinkGhost;


    private bool powerMode = false;
    private bool goForward = false;
    private bool goBackward = false;
    private bool goRight = false;
    private bool goLeft = false;
    private int score = 0;

    void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        pinkGhostAgent = this.pinkGhost.GetComponent<NavMeshAgent>();
        pinkGhostAgent.speed = 10.0f;

        this.theScoreTextMesh = this.ScoreText.GetComponent<TextMesh>();
        this.ghostModeTextMesh = this.ModeText.GetComponent<TextMesh>();
    }

    void Start()
    {
        this.theScoreTextMesh.text = "Score: " + score;
        this.ghostModeTextMesh.text = "Ghost: Deadly!";
    }

    IEnumerator PowerPellet()
    {
        powerMode = true;
        this.ghostModeTextMesh.text = "Ghost: Edible!";
        yield return new WaitForSeconds(10);
        this.ghostModeTextMesh.text = "Ghost: Deadly!";
        powerMode = false;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Pellet"))
        {
            score++;
            this.theScoreTextMesh.text = "Score: " + score;
        }
        else if(collision.gameObject.tag.Equals("RightTeleporter"))
        {
            this.gameObject.transform.position = new Vector3(-11.5f, .15f,0.5f);
        }
        else if(collision.gameObject.tag.Equals("LeftTeleporter"))
        {
           this.gameObject.transform.position = new Vector3(11.5f, .15f, 0.5f);
        }
        else if(collision.gameObject.tag.Equals("PowerPellet"))
        {
            StartCoroutine(PowerPellet()); 
        } 
        else if(collision.gameObject.tag.Equals("Ghost"))
        {
            if(powerMode == false)
            {
                Destroy(this.gameObject);
            }
            else if(powerMode == true)
            {
                Destroy(collision.gameObject);
                score = score + 100;
                this.theScoreTextMesh.text = "Score: " + score;
            }
        }
    }
    
    void Update()
    {
        this.pinkGhostAgent.SetDestination(this.gameObject.transform.position);

        if (goForward)
        {
            rb.velocity = Vector3.forward * speed;
        }
        else if(goBackward)
        {
            rb.velocity = Vector3.back * speed;
        }
        else if(goLeft)
        {
            rb.velocity = Vector3.left * speed;
        }
        else if(goRight)
        {
            rb.velocity = Vector3.right * speed;
        }

        if (Input.GetKeyDown("up"))
        {
            this.transform.rotation = Quaternion.LookRotation(Camera.main.transform.up);
            goForward = true;
            goBackward = false;
            goRight = false;
            goLeft = false;
            
        }
        else if (Input.GetKeyDown("down"))
        {
            this.transform.rotation = Quaternion.LookRotation(-Camera.main.transform.up);

            goForward = false;
            goBackward = true;
            goRight = false;
            goLeft = false;
            
        }
        else if (Input.GetKeyDown("left"))
        {
            this.transform.rotation = Quaternion.LookRotation(-Camera.main.transform.right);

            goForward = false;
            goBackward = false;
            goRight = false;
            goLeft = true;
            
        }
        else if (Input.GetKeyDown("right"))
        {
            this.transform.rotation = Quaternion.LookRotation(Camera.main.transform.right);

            goForward = false;
            goBackward = false;
            goRight = true;
            goLeft = false;
            
        }
    }
}