using System;
using System.Collections;
using UnityEngine;

public class nut_PRoj : MonoBehaviour
{
    
    private Transform player;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private IEnumerator destroyNut()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    private IEnumerator destroyNut1()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
    void Start()
    {
        StartCoroutine(destroyNut());
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
            
            float intialPos = transform.position.x;
            float finalPos = player.transform.position.x;
            float intialPosY = transform.position.y;
            float finalPosY = player.transform.position.y;
            float dis = Mathf.Abs(finalPos - intialPos);
            float disY = Mathf.Abs(finalPosY - intialPosY);
            float intialV = MathF.Sqrt((dis * dis * 9.8f)/(dis - disY));

            if (intialPos > finalPos)
            {
                rb.linearVelocity = new Vector2(-intialV * 0.707f, intialV * 0.707f);
            }
            else
            {
                rb.linearVelocity = new Vector2(intialV * 0.707f, intialV * 0.707f);
            }


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Manager.Instance.RespawnPlayer();
            StartCoroutine(destroyNut1());
        }
    }
}
