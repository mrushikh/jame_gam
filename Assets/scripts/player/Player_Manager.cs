using System.Collections;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{

    public static Player_Manager Instance { get; private set; }

    [Header("Player Settings")]
    public GameObject player;

    [Header("Respawn Settings")]
    public Transform playerSpawn;
    public float respawnTime = 0.5f;


    #region Singlton


    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerSpawn = GameObject.FindWithTag("Respawn").gameObject.transform;
        player.transform.position = playerSpawn.transform.position;
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
        StartCoroutine(respawnTimer());
    }

    private IEnumerator respawnTimer()
    {
        player.SetActive(false);
        yield return new WaitForSeconds(respawnTime);   
        player.SetActive(true);
        player.transform.position = playerSpawn.position;

    }






}
