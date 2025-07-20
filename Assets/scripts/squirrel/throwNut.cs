using UnityEngine;

public class throwNut : MonoBehaviour
{
    public GameObject nutProj;
    [SerializeField] private float cooldown;
    private float cooldownTime;
    private Transform player;
    private float dis;
    [SerializeField] private float Range = 6f;
    public bool showRange = false;
    public bool playerInBound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cooldownTime = cooldown;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }
    private void OnDrawGizmos()
    {
        if (showRange)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, Range);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= Range)
        {
            playerInBound = true;
        }
        else { 
            playerInBound=false;
        }

        if (player != null)
        {
            float intialPos = transform.position.x;
            float finalPos = player.transform.position.x;
            dis = Mathf.Abs(finalPos - intialPos);
        }


        if (cooldownTime > 0)
        {
            cooldownTime -= Time.deltaTime;
        }
        else if(playerInBound) { 
            cooldownTime = cooldown;
            if (dis<10) {
                Instantiate(nutProj, transform.position, Quaternion.identity);

            }

        }
    }
}
