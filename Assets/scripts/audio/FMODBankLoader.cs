using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;

public class FMODBankLoader : MonoBehaviour
{
    [BankRef]
    public List<string> Banks;

    public string Scene = "SampleScene";

    private bool banksLoaded = false;

    public bool IsBanksLoaded()
    {
        return banksLoaded;
    }

    void Start()
    {
        StartCoroutine(LoadBanks());
    }

    IEnumerator LoadBanks()
    {
        foreach (var bank in Banks)
        {
            RuntimeManager.LoadBank(bank, true);
        }

        while (RuntimeManager.AnySampleDataLoading())
        {
            yield return null;
        }

        banksLoaded = true;
    }

    public void OnStartButtonClicked()
    {
        if (!banksLoaded)
        {
            Debug.LogWarning("FMOD banks are still loading.");
            return;
        }

        // Play all emitters in the current scene
        StudioEventEmitter[] emitters = Object.FindObjectsByType<StudioEventEmitter>(FindObjectsSortMode.None);
        foreach (var emitter in emitters)
        {
            emitter.Play();
        }

        // Optionally wait a bit if you want emitters to play before the scene changes
        StartCoroutine(LoadSceneAfterDelay(1.0f));
    }

    // Optional coroutine if you want to delay scene load
    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(Scene);
    }
}