using UnityEngine;

public class shotBall : MonoBehaviour
{   
    private Rigidbody2D rb;

    [Header("Pojectiel Settings")]
    [SerializeField] private float projectileSpeed = 2f;
    public bool canMove = false;

    void Update()
    {
        if (canMove)
        {
            handleMove();
        }
    }

    private void handleMove()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.linearVelocityX = -projectileSpeed;
    }


}
