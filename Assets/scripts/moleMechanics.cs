using UnityEngine;
using System.Collections;
using FMODUnity;
using FMOD.Studio;

public class moleMechanics : MonoBehaviour
{
    private Transform player;
    private Transform ground;

    [Header("Mole Settings")]
    private Vector3 targetPosition;
    private Vector3 originalPosition;

    
    [SerializeField] private float moleRange = 4.5f; // how far the player is before mole pops up

    [SerializeField] private float origCooldown = 5f;
    [SerializeField] private float cooldown = 5f;

    [SerializeField] public ParticleSystem moleDustParticles;
    private ParticleSystem activeParticles;
    private bool dustActive = false;

    public bool showRange = true;
    private Rigidbody2D mole;
    private Animator moleAnim;
    private SpriteRenderer moleSpriteRenderer;

    public StudioEventEmitter moleDig;
    public StudioEventEmitter molePopUp;
    public StudioEventEmitter molePopDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mole = gameObject.GetComponent<Rigidbody2D>();
        moleAnim = gameObject.GetComponent<Animator>();
        moleSpriteRenderer= gameObject.GetComponent<SpriteRenderer>();
        player = Player_Manager.Instance.player.transform;
        
    }

    private void OnDrawGizmos()
    {
        if (showRange)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, moleRange);
        }
    }

   //private IEnumerator particleSpa()
   // {   dustActive = true;
   //     if (moleDustParticles != null)
   //     {
   //         activeParticles = Instantiate(moleDustParticles, transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);

   //         Destroy(activeParticles.gameObject, activeParticles.main.duration);
            
   //         yield return new WaitForSeconds(1);
   //         activeParticles = Instantiate(moleDustParticles, transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);

   //         Destroy(activeParticles.gameObject, activeParticles.main.duration);
           
   //     }
   //     yield return new WaitForSeconds(0.5f);
   //     dustActive = false;
   // }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown<3&& (Vector3.Distance(player.position, transform.position) <= moleRange)) {

            if (dustActive==false&&moleDustParticles != null&&cooldown>2.6)
            {
                dustActive = true;
                activeParticles = Instantiate(moleDustParticles, transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
                Destroy(activeParticles.gameObject, activeParticles.main.duration);
            }

            if (cooldown<2.5) {
                if (cooldown > 1)
                {
                    dustActive = false;
                }
                
                moleAnim.SetBool("goingUp", true);
                moleSpriteRenderer.enabled = true;

                if (cooldown<0.3f) {
                    if (dustActive == false && moleDustParticles != null)
                    {
                        dustActive = true;
                        activeParticles = Instantiate(moleDustParticles, transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
                        Destroy(activeParticles.gameObject, activeParticles.main.duration);
                    }
                }
                if (cooldown<0) { 
                    cooldown = origCooldown;
                    dustActive=false;
                    molePopDown.Play();
                    moleDig.Play();
                }
            }
        }
        if (cooldown>2.1f) {
            moleAnim.SetBool("goingUp", false);
            moleSpriteRenderer.enabled = false;
        }

        if (cooldown<1.9f) {
            moleDig.Stop();
        }

        if (cooldown<1f) {
            molePopUp.Play();
        }

       
        
    }

}
