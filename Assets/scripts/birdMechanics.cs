using UnityEngine;

public class birdMechanics : MonoBehaviour
{
    public Transform player;
    public Transform ground;
    public GameObject stick;
    public Vector3 abovePlayerOffset;   // how far the bird should stop above the player

    private Rigidbody2D bird;
    private float birdSpeed = 4f;
    private bool playerInBounds = false;
    private bool stickDropped = false;
    private float counter = 6f; // how long the bird should follow the player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bird = gameObject.GetComponent<Rigidbody2D>();
        abovePlayerOffset = new Vector3(0, 5, 0);
    }

    void DropStick()
    {
        Instantiate(stick, transform.position, Quaternion.identity);
        stickDropped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= 6f)
            playerInBounds = true;

        if (playerInBounds)
        {
            counter -= Time.deltaTime;
            if (counter > 0)
            {
                // following the player
                Vector3 targetPosition = player.position + abovePlayerOffset;
                transform.position = Vector3.Lerp(transform.position, targetPosition, birdSpeed * Time.deltaTime);
            }
            else if (!stickDropped)
            {
                DropStick();
            }
        }
    }
}
