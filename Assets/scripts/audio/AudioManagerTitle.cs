using UnityEngine;
using FMODUnity;

public class AudioManagerTitle : MonoBehaviour
{
    public FMODBankLoaderTitle bankLoader;
    public StudioEventEmitter ambienceEmitter;

    private bool hasStarted = false;

    void Update()
    {
        if (!hasStarted && bankLoader != null && bankLoader.IsBanksLoaded())
        {
            ambienceEmitter.Play();
            hasStarted = true;
        }
    }

    void OnDestroy()
    {
        if (hasStarted)
        {
            ambienceEmitter?.Stop();
            hasStarted = false;
        }
    }
}