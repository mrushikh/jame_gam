using UnityEngine;
using FMODUnity;
using FMOD.Studio;

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
    private Animator animatorSq;

    public StudioEventEmitter squirrelScream;
    public StudioEventEmitter squirrelThrowNut;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cooldownTime = cooldown;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animatorSq = GetComponent<Animator>();
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

            if (cooldownTime < 1.4f && cooldownTime > 1.3f) {
                squirrelScream.Play();
            }

            if (cooldownTime < 0.8f&&animatorSq.GetBool("throwing")==false) {
                animatorSq.SetBool("throwing", true);
            }

            if (cooldownTime < 0.1f) {
                squirrelThrowNut.Play();
            }
        }   
        else if(playerInBound) { 
            cooldownTime = cooldown;
            animatorSq.SetBool("throwing", false);
            Instantiate(nutProj, transform.position, Quaternion.identity);

            

        }
    }
}
