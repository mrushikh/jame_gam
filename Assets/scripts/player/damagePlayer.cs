using UnityEngine;
using UnityEngine.SceneManagement;

public class damagePlayer : MonoBehaviour
{    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Manager.Instance.RespawnPlayer();
        }
    }
}
