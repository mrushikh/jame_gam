using UnityEngine;
using System.Collections;

public class birdMechanics : MonoBehaviour
{
    public Transform player;
    public Transform ground;
    public GameObject stick;
    public Vector3 abovePlayerOffset;   // how far the bird should stop above the player

    // bird 
    [Header("Bird Settings")]
    private Rigidbody2D bird;
    [SerializeField] private float birdSpeed = 4f;
    [SerializeField] private float birdRange = 6f;
    [SerializeField] private float birdFollowTime = 6f;
    [SerializeField] private float birdCooldown = 4f;
    public bool showRange = false;

    private bool playerInBounds = false;
    private bool stickDropped = false;
    private float counter = 6f; // how long the bird should follow the player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bird = gameObject.GetComponent<Rigidbody2D>();
        abovePlayerOffset = new Vector3(0, 5, 0);
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
    }

    // Update is called once per frame
    void Update()
    {
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
                stickDropped = false;
                counter = birdFollowTime;
                birdCooldown = 4f;
            }
        }
    }
}
