using UnityEngine;

public class throwNut : MonoBehaviour
{
    public GameObject nutProj;
    [SerializeField] private float cooldown;
    private float cooldownTime;
    private Transform player;
    private float dis;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cooldownTime = cooldown;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null){
            float intialPos = transform.position.x;
            float finalPos = player.transform.position.x;
            dis = Mathf.Abs(finalPos - intialPos);
        }


        if (cooldownTime > 0)
        {
            cooldownTime -= Time.deltaTime;
        }
        else { 
            cooldownTime = cooldown;
            if (dis<10) {
                Instantiate(nutProj, transform.position, Quaternion.identity);

            }

        }
    }
}
