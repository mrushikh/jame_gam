using JetBrains.Annotations;
using System.Collections;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class playerMovement : MonoBehaviour
{   //movement
    private Rigidbody2D rb;
    private float moveX;
    private Vector2 movement;
    public float MoveSpeed = 5f;
    private SpriteRenderer SpriteRenderer;
    private bool leftfacing = false;
    private bool onPlatform = false;

    //jump
    public float JumpForce = 10f;
    public LayerMask GroundLayer;
    public BoxCollider2D GroundCollider;
    private bool OnGround;
    public StudioEventEmitter PlayerJump;

    //dashing
    public bool canDash=false;
    private bool isDashing;
    public float dashingPower = 10f;
    public float vertDashingPower = 10f;
    private float dashingTime = 0.1f;
    private float dashingCooldown = 1f;

    //umbrella
    public GameObject umbrellaPivot;
    public GameObject umbrella;

    public Transform respawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = rb.GetComponent<SpriteRenderer>();
        OnGround = false;
        canDash = false;    
        umbrella.SetActive(false);
    }
    
    private IEnumerator umbrellaDir()
    {   if (!leftfacing)
        {
            umbrellaPivot.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else {
            umbrellaPivot.transform.eulerAngles = new Vector3(0, 0, 180);
        }

        if (math.abs(Input.GetAxisRaw("Vertical")) > 0)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                umbrellaPivot.transform.eulerAngles = new Vector3(0, 0, 90);
            }
            else
            {
                umbrellaPivot.transform.eulerAngles = new Vector3(0, 0, -90);
            }

        }
     
        umbrella.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        umbrella.SetActive(false);
    }
    public IEnumerator Dash(string dir)
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (dir == "up")
        {
            rb.linearVelocity = new Vector2(0f,vertDashingPower);
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX * dashingPower, 0f);
        }
            
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing=false;
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {   

        // Check if we collided with the ground
        if (GroundLayer == (1 << other.gameObject.layer))
        {
            OnGround = true;

        }

        if (other.CompareTag("spikes"))
        {
            Debug.Log("ded");
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        moveX = Input.GetAxisRaw("Horizontal");
        if (moveX < 0)
        {
            if (leftfacing == false)
            {
                transform.Rotate(0, 180, 0);
                leftfacing = true;
            }

        }
        else if (moveX > 0)
        {
            if (leftfacing == true)
            {
                transform.Rotate(0, 180, 0);
                leftfacing = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.Space) && OnGround)
        {
            // Make our player jump
            rb.linearVelocity = new Vector2(rb.linearVelocityX, JumpForce);
            OnGround = false;
            PlayerJump.Play();

        }

        
        //dash
        if(Input.GetKeyDown(KeyCode.Mouse0) && canDash)
        {
            if (math.abs(Input.GetAxisRaw("Vertical")) > 0||math.abs(moveX)==0f)
            {
                StartCoroutine(Dash("up"));
            }
            else {
                StartCoroutine(Dash("notUp"));
            }
           
            
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        //umbrella
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(umbrellaDir());
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        // Determine our movement vector based on our speed and move inputs
        movement = new Vector2(moveX * MoveSpeed, rb.linearVelocityY);

        // Make our player move left/right
        rb.linearVelocity = movement;
    }
}
