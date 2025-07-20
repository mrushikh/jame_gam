using UnityEngine;

public class respawnNewPoint : MonoBehaviour
{
    public GameObject respawnPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player_Manager.Instance.respawnPoint = respawnPoint.transform;
        }
    }
}
