using UnityEngine;

public class Player_Manager : MonoBehaviour
{

    public static Player_Manager Instance { get; private set; }

    [Header("Player Settings")]
    public GameObject player;
    public Transform playerSpawn;

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
        player.transform.position = playerSpawn.position;
    }






}
