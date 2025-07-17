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
   

    //umbrella
    public GameObject umbrellaPivot;
    public GameObject umbrella;
    public StudioEventEmitter umbrellaOpenGlide;
    public float bounceUmbrPwr;

    private bool isGliding;
    
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
    {   
        rb.gravityScale = 2f;
        if (!leftfacing)
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
        float originalGravity = 2f;
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
    public IEnumerator bounceUmbr()
    {
        rb.linearVelocity = new Vector2(0f,bounceUmbrPwr);
        isDashing=true;
        yield return new WaitForSeconds(0.2f);
        isDashing=false;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {   

        // Check if we collided with the ground
        if (GroundLayer == (1 << other.gameObject.layer))
        {
            OnGround = true;
            umbrellaOpenGlide.SetParameter("GroundCollide", 1.0f);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GroundLayer == (1 << collision.gameObject.layer))
        {
            OnGround = false;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if(rb.gravityScale == 0.2f)
        {
            isGliding = true;
        }
        else
        {
            isGliding = false;
        }
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (OnGround)
            {
                OnGround = false;
                rb.gravityScale = 2;
                // Make our player jump
                rb.linearVelocity = new Vector2(rb.linearVelocityX, JumpForce);
                PlayerJump.Play();
            }
            else 
            {   
                rb.linearVelocityY = 0;
                rb.gravityScale = 0.2f;
                umbrellaOpenGlide.SetParameter("GroundCollide", 0.0f);
                umbrellaOpenGlide.Play();
            }



        }

        if (Input.GetKeyUp(KeyCode.Space)) {

            rb.gravityScale = 2;
            umbrellaOpenGlide.Stop();
        }

        //dash
        if (Input.GetKeyDown(KeyCode.Mouse0) && canDash)
        {
            if (math.abs(Input.GetAxisRaw("Vertical")) > 0 || math.abs(moveX) == 0f)
            {
                StartCoroutine(Dash("up"));
            }
            else
            {
                StartCoroutine(Dash("notUp"));
            }


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
