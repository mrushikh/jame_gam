using UnityEngine;

public class shotBall : MonoBehaviour
{   
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      rb=gameObject.GetComponent<Rigidbody2D>();
      rb.linearVelocityX = -2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
