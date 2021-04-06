using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MouseReader : PointerReader
{
    private void OnMouseDown()
    {
        PointerDown();
    }

    private void OnMouseUp()
    {
        PointerUp();
    }

    private void OnMouseExit()
    {
        PointerExit();
    }

    private void OnMouseEnter()
    {
        PointerEnter();
    }

    private void OnMouseDrag()
    {
        PointerDrag();
    }
}