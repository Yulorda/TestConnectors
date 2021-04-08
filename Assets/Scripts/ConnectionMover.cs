using System.Collections;
using UnityEngine;

public class ConnectionMover : MonoBehaviour
{
    [SerializeField]
    PointerReader pointerReader;

    //TODO заменить на интерфейс и сериализовать
    [SerializeField]
    MovingConnector movableElement;

    private Plane plane;

    private void Awake()
    {
        pointerReader.OnDrag += OnPointerDrag;
        pointerReader.OnDown += OnPointerDown;
        pointerReader.OnUp += OnPointerUp;
    }

    private void OnDestroy()
    {
        pointerReader.OnDrag -= OnPointerDrag;
        pointerReader.OnDown -= OnPointerDown;
        pointerReader.OnUp += OnPointerUp;
    }

    public void OnPointerDown()
    {
        ((IMovable)movableElement).StartMove();
        var position = ((IMovable)movableElement).GetPosition();
        plane = new Plane(Vector3.up, Vector3.up * position.y); // ground plane
    }

    public void OnPointerDrag()
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

    public void OnPointerUp()
    {
        ((IMovable)movableElement).EndMove();
    }
}
