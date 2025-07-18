using Unity.Cinemachine;
using UnityEngine;

public class Scene_Transition : MonoBehaviour
{
    [SerializeField]private BoxCollider2D m_Colider;
    [SerializeField] private CinemachineCamera m_CineCam;
    [SerializeField]private CinemachineConfiner2D m_Confiner;

    private void Start()
    {
        m_Colider = GetComponent<BoxCollider2D>();
        m_CineCam = GetComponentInChildren <CinemachineCamera>();
        m_Confiner = GetComponentInChildren<CinemachineConfiner2D>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_CineCam.Priority++;
            m_Confiner.BoundingShape2D = m_Colider;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            m_CineCam.Priority--;
        }
    }


}
