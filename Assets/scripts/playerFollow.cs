using UnityEngine;

public class playerFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float smoothSpeed = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void LateUpdate()
    {
        Vector3 desired = playerTransform.position + offset;
        desired.y = 0f;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPos;
    }
}

