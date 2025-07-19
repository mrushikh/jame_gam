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
    private float cooldown = 5f;
    private bool reset = false;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        player = Player_Manager.Instance.player.transform;
        originalPosition = transform.position;
        Instantiate(rb, originalPosition, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !platformActive)
        {
            platformActive = true;
            Debug.Log("KFJSDLKFJKLDSJFKLDS");
        }
        if (collision.gameObject.CompareTag("ground"))
        {
            Destroy(gameObject);    // destroys on any collision
            reset = true;
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
        {
            cooldown -= Time.deltaTime;
            if (cooldown < 0)
            {
                cooldown = 5f;
                platformActive = false;
                shakeTime = origShakeTime;
                Instantiate(rb, originalPosition, Quaternion.identity);
                reset = false;
            }
        }
    }
}