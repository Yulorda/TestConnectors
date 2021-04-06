using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PointerReader))]
[RequireComponent(typeof(ElementModel))]
public class StageMover : MonoBehaviour
{
    [SerializeField]
    PointerReader pointerReader;

    //TODO Сераиализовать интерфейс
    [SerializeField]
    ElementModel movableElement;

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

    private void OnPointerDown()
    {
        ((IMovable)movableElement).StartMove();
        var position = ((IMovable)movableElement).GetPosition();
        plane = new Plane(Vector3.up, Vector3.up * position.y); // ground plane
    }

    private void OnPointerDrag()
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

    private void OnPointerUp()
    {
        ((IMovable)movableElement).EndMove();
    }
}
