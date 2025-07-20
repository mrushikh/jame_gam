using UnityEngine;
using System.Collections;
using FMODUnity;
using FMOD.Studio;

public class birdMechanics : MonoBehaviour
{
    private Transform player;
    private Transform ground;
    public GameObject stick;
    public Vector3 abovePlayerOffset;   // how far the bird should stop above the player

    // bird 
    [Header("Bird Settings")]
    [SerializeField] private float birdSpeed = 4f;
    [SerializeField] private float birdRange = 6f;
    [SerializeField] private float birdFollowTime = 6f;
    [SerializeField] private float birdCooldown = 4f;
    private Rigidbody2D bird;
    public bool showRange = false;

    private bool playerInBounds = false;
    private bool stickDropped = false;
    private float counter = 6f; // how long the bird should follow the player

    // audio

    public StudioEventEmitter birdWings;
    public StudioEventEmitter birdScream;
    public StudioEventEmitter birdDropBranch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bird = gameObject.GetComponent<Rigidbody2D>();
        abovePlayerOffset = new Vector3(0, 4, 0);
        player = Player_Manager.Instance.player.transform;
    }

    private void OnDrawGizmos()
    {
        if (showRange)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, birdRange);
        }
    }

    void DropStick()
    {
        Instantiate(stick, transform.position, Quaternion.identity);
        stickDropped = true;
        birdDropBranch.Play();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (playerInBounds)
        {
            SpriteRenderer spriteBird = gameObject.GetComponent<SpriteRenderer>();
            spriteBird.enabled = true;
            
            if (!birdWings.IsPlaying()) birdWings.Play();
            if (!birdScream.IsPlaying()) birdScream.Play();
        }
        else {
            SpriteRenderer spriteBird = gameObject.GetComponent<SpriteRenderer>();
            spriteBird.enabled = false;
            birdWings.Stop();
        }

        if (Vector3.Distance(player.position, transform.position) <= birdRange)
            playerInBounds = true;

        if (playerInBounds)
        {   
            counter -= Time.deltaTime;
            if (counter > 0)
            {
                // following the player
                Vector3 targetPosition = player.position + abovePlayerOffset;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, birdSpeed * Time.deltaTime);
            }
            else if (!stickDropped)
                DropStick();
        }
        if (stickDropped)
        {
            birdCooldown -= Time.deltaTime;
            if (birdCooldown < 0)
            {
                // reset bird mechanics
                playerInBounds = false;
                birdWings.Stop();
                birdScream.Stop();
                stickDropped = false;
                counter = birdFollowTime;
                birdCooldown = 4f;
            }
        }
    }
}
