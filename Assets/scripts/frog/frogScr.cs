using UnityEngine;

public class frogScr : MonoBehaviour
{   private Rigidbody2D rigidbody2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        rigidbody2.AddForce(new Vector2(-100, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Manager.Instance.RespawnPlayer();
        }
    }
}
