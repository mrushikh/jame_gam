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
    
    private bool OnGround;
    public StudioEventEmitter PlayerJump;
    public Vector2 boxSize;
    public float castDistance;

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
    public bool downUmbr;
    [SerializeField] private float umbrellaCooldown = 1.5f;
    private bool canUmbre;

    //umbrellaImg
    public GameObject umbrellaImgPivot;
    public SpriteRenderer umbrellaImg;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        canUmbre = true;
        rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = rb.GetComponent<SpriteRenderer>();
        OnGround = false;
        canDash = false;    
        downUmbr = false;
        umbrella.SetActive(false);
        umbrellaImg.enabled = false;
    }
    
    private IEnumerator umbrellaDir()
    {
        umbrellaCooldown = 2f;
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
                downUmbr = true;
                umbrellaPivot.transform.eulerAngles = new Vector3(0, 0, -90);
                
            }

        }
     
        umbrella.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        downUmbr=false;
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
            umbrellaImgPivot.transform.eulerAngles = new Vector3(0, 0, -90); ;
            
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX * dashingPower, 0f);
            if (leftfacing)
            {
                umbrellaImgPivot.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else {
                umbrellaPivot.transform.eulerAngles = new Vector3(0, 0, -90);
            }
        }
        
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing=false;
       

    }
    public void bounceUmbr()
    {
        rb.linearVelocity = new Vector2(0f,bounceUmbrPwr);
        
    }
   public void isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up,castDistance,GroundLayer))
        {
            OnGround = true;
            umbrellaOpenGlide.Stop();
        }
        else
        {
            OnGround=false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up*castDistance, boxSize);
    }
    // Update is called once per frame
    void Update()
    {
        if (rb.gravityScale == 0.2f)
        {   
            umbrellaImgPivot.transform.eulerAngles =new Vector3(0, 0, 90); ;
            umbrellaImg.enabled = true;
        }
        else
        {
            umbrellaImg.enabled=false;
        }

        
        //umbrella cooldown
        if (umbrellaCooldown > 0)
        {
            umbrellaCooldown -= Time.deltaTime;
            canUmbre = false;
        }
        else
        {   
            canUmbre = true;
        }
        //groundCheck
        isGrounded();

        if (isDashing)
        {
            umbrellaImg.enabled = true;
            return;
        }else if(rb.gravityScale != 0.2f)
        {
            umbrellaImg.enabled = false;
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
        if (Input.GetKeyDown(KeyCode.Mouse1)&&canUmbre==true)
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
