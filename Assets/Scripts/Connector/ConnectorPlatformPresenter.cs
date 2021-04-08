using UnityEngine;

public class ConnectorPlatformPresenter : MonoBehaviour
{
    //TODO заменить на интерфейс и сериализовать
    [SerializeField]
    MovingConnector movingConnector;

    [SerializeField]
    EmissionHighlighter highlighter;

    private Vector3 distance;

    private void OnMouseUp()
    {
        ((IMovable)movingConnector).EndMove();
        highlighter.Highlight(false);
    }

    private void OnMouseDown()
    {
        highlighter.Highlight(true);
        ((IMovable)movingConnector).StartMove();
        var position = ((IMovable)movingConnector).GetPosition();
        distance = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(position).z)) - position;
    }

    private void OnMouseDrag()
    {
        var position = ((IMovable)movingConnector).GetPosition();
        Vector3 distance_to_screen = Camera.main.WorldToScreenPoint(position);
        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen.z));
        ((IMovable)movingConnector).SetPosition(new Vector3(pos_move.x - distance.x, position.y, pos_move.z - distance.z));
    }
}
