using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOnCanvas : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Camera drawingCamera;
    public float lineThickness = 0.1f;

    private List<Vector3> drawingPositions = new List<Vector3>();

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = GetTouchPosition(touch.position);
                drawingPositions.Add(touchPosition);
                lineRenderer.positionCount = drawingPositions.Count;
                lineRenderer.SetPosition(drawingPositions.Count - 1, touchPosition);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector3 touchPosition = GetTouchPosition(touch.position);
                drawingPositions.Add(touchPosition);
                lineRenderer.positionCount = drawingPositions.Count;
                lineRenderer.SetPosition(drawingPositions.Count - 1, touchPosition);
            }
        }
    }

    Vector3 GetTouchPosition(Vector2 touchPosition)
    {
        Vector3 worldPosition = drawingCamera.ScreenToWorldPoint(touchPosition);
        worldPosition.z = 0f;
        return worldPosition;
    }
}
