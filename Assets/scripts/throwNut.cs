using UnityEngine;

public class throwNut : MonoBehaviour
{
    public GameObject nutProj;
    [SerializeField] private float cooldown;
    private float cooldownTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cooldownTime = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTime > 0)
        {
            cooldownTime -= Time.deltaTime;
        }
        else { 
            cooldownTime = cooldown;
            Instantiate(nutProj,transform.position,Quaternion.identity);
        
        }
    }
}
