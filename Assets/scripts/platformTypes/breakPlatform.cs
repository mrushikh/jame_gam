using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class breakPlatform : MonoBehaviour
{
    private Transform player;
    private Transform ground;

    [Header("Breakable Platform Settings")]
    private Vector3 originalPosition;

    [SerializeField] private float popUpHeight = 0.3f;

    private Rigidbody2D platform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platform = gameObject.GetComponent<Rigidbody2D>();
        player = Player_Manager.Instance.player.transform;
        originalPosition = transform.position;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("FKLDSJFKLJSDKLFJDL");
        }
    }

    void Update()
    {
        
    }
}