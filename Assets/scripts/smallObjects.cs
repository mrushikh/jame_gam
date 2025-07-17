using UnityEngine;
using UnityEngine.SceneManagement;

public class smallObjects : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("DKJFSKLJFJDSLFSLKDF");
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
        }

        Destroy(gameObject);    // destroys on any collision or after 10 seconds
    }
}