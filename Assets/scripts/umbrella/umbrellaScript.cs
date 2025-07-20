using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class umbrellaScript : MonoBehaviour
{   public playerMovement playerMovementScr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public StudioEventEmitter umbrellaBlockBounce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("damageBall") || collision.tag.Equals("stick")|| collision.tag.Equals("nut"))
        {
            Destroy(collision.gameObject);
            umbrellaBlockBounce.Play();
        }
            
        if (collision.tag.Equals("spikes") && playerMovementScr.downUmbr == true)
        {
            playerMovementScr.bounceUmbr();
            umbrellaBlockBounce.Play();
        }
    }

}
