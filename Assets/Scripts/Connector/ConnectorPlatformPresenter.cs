using UnityEngine;

public class ConnectorPlatformPresenter : MonoBehaviour
{
    //TODO заменить на интерфейс и сериализовать
    [SerializeField]
    MovingConnector movableElement;

    [SerializeField]
    EmissionHighlighter highlighter;

    private Plane plane;
    
    private void OnMouseUp()
    {
        ((IMovable)movableElement).EndMove();
        highlighter.Highlight(false);
    }

    private void OnMouseDown()
    {
        highlighter.Highlight(true);
        ((IMovable)movableElement).StartMove();
        var position = ((IMovable)movableElement).GetPosition();
        plane = new Plane(Vector3.up, Vector3.up * position.y); // ground plane
    }

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance; // the distance from the ray origin to the ray intersection of the plane
        if (plane.Raycast(ray, out distance))
        {
            Vector3 rayPoint = ray.GetPoint(distance);
            Vector3 snappedRayPoint = rayPoint;

            ((IMovable)movableElement).SetPosition(rayPoint);
        }
    }
}
