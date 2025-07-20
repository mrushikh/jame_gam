using UnityEngine;
using UnityEngine.SceneManagement;

public class canvasSetup : MonoBehaviour
{
    Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        // Make sure we re-assign when a new scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Also do it right now for the initial scene
        AssignCamera();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignCamera();
    }

    void AssignCamera()
    {
        // Find your main camera however you like
        Camera cam = Camera.main;  // or GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        if (cam == null) return;

        // Set the canvas to render in camera space:
        _canvas.renderMode = RenderMode.ScreenSpaceCamera;
        _canvas.worldCamera = cam;
        _canvas.planeDistance = 1f; // optional: how far in front of the camera to draw
    }

}
