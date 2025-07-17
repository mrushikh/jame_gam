using UnityEngine;

public class umbrellaScript : MonoBehaviour
{   public playerMovement playerMovementScr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("damageBall"))
        {
            playerMovementScr.canDash = true;
            
            Destroy(collision.gameObject);
            
        }
        if (collision.tag.Equals("spikes")&&playerMovementScr.downUmbr==true)
        {   
            playerMovementScr.bounceUmbr();
            playerMovementScr.canDash = true;
            
        }
    }

}
