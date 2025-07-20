using UnityEngine;

public class respawnNewPoint : MonoBehaviour
{
    public GameObject respawnPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            respawnPoint.transform.position=gameObject.transform.position;   
        }
    }
}
