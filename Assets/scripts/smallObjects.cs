using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class smallObjects : MonoBehaviour
{
    private IEnumerator destroyNut()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
    private void Start()
    {
        StartCoroutine(destroyNut());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Manager.Instance.RespawnPlayer();
            Destroy(gameObject);
        }
           // destroys on any collision
    }
}