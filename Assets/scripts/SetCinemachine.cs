using Unity.Cinemachine;
using UnityEngine;

public class SetCinemachine : MonoBehaviour
{
    CinemachineCamera _cineCam;

    private void Awake()
    {
        _cineCam = GetComponent<CinemachineCamera>();
    }

    private void Start()
    {
        // Grab the player’s transform from your manager
        var playerT = Player_Manager.Instance.player.transform;

        // Tell Cinemachine to follow AND look at the player
        _cineCam.Follow = playerT;
        _cineCam.LookAt = playerT;
    }
}
