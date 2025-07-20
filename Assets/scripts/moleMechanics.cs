using UnityEngine;
using System.Collections;

public class moleMechanics : MonoBehaviour
{
    private Transform player;
    private Transform ground;

    [Header("Mole Settings")]
    private Vector3 targetPosition;
    private Vector3 originalPosition;

    [SerializeField] private float popUpHeight = 0.3f;
    [SerializeField] private float popUpSpeed = 5f;
    [SerializeField] private float moleRange = 4.5f; // how far the player is before mole pops up
    [SerializeField] private float popUpTime = 3f;
    [SerializeField] private float origPopUpTime = 3f;
    [SerializeField] private float origCooldown = 2f;
    [SerializeField] private float cooldown = 2f;

    [SerializeField] public ParticleSystem moleDustParticles;
    private ParticleSystem activeParticles;
    private bool dustActive = false;

    public bool showRange = true;
    private Rigidbody2D mole;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mole = gameObject.GetComponent<Rigidbody2D>();
        player = Player_Manager.Instance.player.transform;
        originalPosition = transform.position;
        targetPosition = transform.position + new Vector3(0f, popUpHeight, 0f);
    }

    private void OnDrawGizmos()
    {
        if (showRange)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, moleRange);
        }
    }

    void Update()
    {
        if ((Vector3.Distance(player.position, transform.position) <= moleRange) && popUpTime > 0 && cooldown == origCooldown)
        {
            if (!dustActive && moleDustParticles != null)
            {
                activeParticles = Instantiate(moleDustParticles, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
                // activeParticles.Play();
                Destroy(activeParticles.gameObject, activeParticles.main.duration);
                dustActive = true;
            }
            popUpTime -= Time.deltaTime;
            if (popUpTime < 2) 
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, popUpSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, popUpSpeed * Time.deltaTime);
            dustActive = false;
        }
        // timing
        if (popUpTime < 0)
        {
            cooldown -= Time.deltaTime;
        }
        if (cooldown < 0)
        {
            // reset popup and cooldown time
            cooldown = origCooldown;
            popUpTime = origPopUpTime;
        }
        
    }
}
