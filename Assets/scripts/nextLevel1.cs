using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel1 : MonoBehaviour
{
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(3);
        }
    }
}
