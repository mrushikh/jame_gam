using UnityEngine;
using UnityEngine.SceneManagement;

public class damagePlayer : MonoBehaviour
{
    public playerMovement playerMovementScr; 
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ba");
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);

        }
    }
}
