using System.Collections;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Player_Manager : MonoBehaviour
{

    public static Player_Manager Instance { get; private set; }

    [Header("Player Settings")]
    public GameObject player;

    [Header("Respawn Settings")]
    public Transform respawnPoint;
    public float respawnDelay = 0.5f;
    private bool isRespawning = false;

    private Animator playerAnim;

    public StudioEventEmitter playerKilled;

    #region Singlton


    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        respawnPoint = GameObject.FindWithTag("Respawn").gameObject.transform;
        playerAnim = player.GetComponentInChildren<Animator>();
        player.transform.position = respawnPoint.transform.position;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion


    public void RespawnPlayer()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        isRespawning = true;

        // 1) Play SFX + fire death anim
        playerKilled.Play();
        player.GetComponent<playerMovement>().enabled = false;
        playerAnim.SetTrigger("onDeath");

        // 2) Grab the length of the currently playing death clip
        float deathLength = 0f;
        var clips = playerAnim.GetCurrentAnimatorClipInfo(0);
        for (int i = 0; i < clips.Length; i++)
        {
            if (clips[i].clip.name == "Player_Death")
            {
                deathLength = clips[i].clip.length;
                break;
            }
        }
        // fallback if we couldn’t find it:
        if (deathLength <= 0f) deathLength = 1f;

        // 3) Wait out the animation + any extra delay
        yield return new WaitForSeconds(deathLength + respawnDelay);

        // 4) Teleport the player’s transform
        player.transform.position = respawnPoint.position;

        // 5) Reset physics so they don’t immediately fall
        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 1f;
            player.GetComponent<playerMovement>().enabled = true;
        }

        // 6) Snap Animator back to Idle (no blends)
        playerAnim.ResetTrigger("onDeath");
        playerAnim.Play("Player_Idle", 0, 0f);
        isRespawning = false;
    }






}
