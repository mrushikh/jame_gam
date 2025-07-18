using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ShowBoxCollider2DGizmo : MonoBehaviour
{
    private BoxCollider2D col;

    public Color color;
    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmos()
    {
        if (col == null) col = GetComponent<BoxCollider2D>();
        Gizmos.color = color;
        // Draw a wireframe box at the collider’s bounds
        Vector2 pos = (Vector2)transform.position + col.offset;
        Gizmos.DrawWireCube(pos, col.size);
    }
}
