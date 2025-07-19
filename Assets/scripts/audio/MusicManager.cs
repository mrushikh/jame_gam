using UnityEngine;
using FMODUnity;

public class MusicManager : MonoBehaviour
{
    public FMODBankLoader bankLoader;
    public StudioEventEmitter musicEmitter;
    public StudioEventEmitter ambienceEmitter;

    private bool hasStarted = false;

    void Update()
    {
        if (!hasStarted && bankLoader != null && bankLoader.IsBanksLoaded())
        {
            ambienceEmitter.Play();
            musicEmitter?.Play();
            hasStarted = true;
        }
    }

    void OnDestroy()
    {
        if (hasStarted)
        {
            ambienceEmitter?.Stop();
            musicEmitter?.Stop();
            hasStarted = false;
        }
    }
}