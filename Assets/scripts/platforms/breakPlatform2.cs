using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class breakPlatform : MonoBehaviour
{
    private Transform player;
    private Transform ground;

    [Header("Breakable Platform Settings")]
    private Vector3 originalPosition;
    [SerializeField] private float shakeTime = 0.5f;
    [SerializeField] private float origShakeTime = 0.5f;
    private float shakeMagnitude = 0.05f;
    private bool platformActive = false;
    [SerializeField] private float cooldown = 2f;
    [SerializeField] private float origCooldown = 2f;
    private bool reset = false;
    private Color platformColour;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.freezeRotation = true;
        player = Player_Manager.Instance.player.transform;
        originalPosition = transform.position;
        platformColour = GetComponent<SpriteRenderer>().color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !platformActive)
        {
            platformActive = true;
        }
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("spikes"))
        {
            reset = true;
            Color tempColour = platformColour;
            tempColour.a = 0f; // make very invisible
            GetComponent<SpriteRenderer>().color = tempColour;
            GetComponent<Collider2D>().isTrigger = true;
        }
    }

    void Update()
    {
        if (platformActive)
        {
            // shake the platform
            if (shakeTime > 0)
            {
                transform.position = originalPosition + new Vector3(Random.Range(-shakeMagnitude, shakeMagnitude), Random.Range(-shakeMagnitude, shakeMagnitude), 0);
                shakeTime -= Time.deltaTime;
            }
            // drop the platform
            else
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.gravityScale = 1f;
            }
        }
        if (reset)
            cooldown -= Time.deltaTime;
        
        if (cooldown < 0f)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
            GetComponent<SpriteRenderer>().color = platformColour;
            GetComponent<Collider2D>().isTrigger = false;
            transform.position = originalPosition;

            cooldown = origCooldown;
            platformActive = false;
            reset = false;
            shakeTime = origShakeTime;
        }
    }
}