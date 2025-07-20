using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class disapPlatform : MonoBehaviour
{
    private Transform player;
    private Transform ground;

    [Header("Diasappearing Platform Settings")]
    [SerializeField] private float disapTime = 1f;
    [SerializeField] private float origDisapTime = 1f;
    [SerializeField] private float appearTime = 2f;
    [SerializeField] private float origAppearTime = 2f;
    private bool platActive = true;
    private Color platformColour;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        player = Player_Manager.Instance.player.transform;
        platformColour = GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        if (platActive)
        {
            appearTime -= Time.deltaTime;
            if (appearTime < 0)
            {
                appearTime = origAppearTime; // reset the appear time
                platActive = false;

                // make platform disappear
                Color tempColour = platformColour;
                tempColour.a = 0f; // make invisible

                GetComponent<SpriteRenderer>().color = tempColour;
                GetComponent<Collider2D>().isTrigger = true;
            }
        }
        else
        {
            disapTime -= Time.deltaTime;
            if (disapTime < 0)
            {
                disapTime = origDisapTime; // reset the appear time
                platActive = true;

                // make platform disappear
                GetComponent<SpriteRenderer>().color = platformColour;
                GetComponent<Collider2D>().isTrigger = false;
            }
        }
    }
}