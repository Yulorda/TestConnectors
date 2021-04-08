using System.Collections;
using UnityEngine;

public class FollowFromMouseConnectorMover : MonoBehaviour
{
    [SerializeField]
    MovingConnector movingConnector;

    private Vector3 distance;

    // Start is called before the first frame update
    public void StartFollow()
    {
        ((IMovable)movingConnector).StartMove();
        var position = ((IMovable)movingConnector).GetPosition();
        distance = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Following();
        StartCoroutine(StartFollowing());
    }

    private IEnumerator StartFollowing()
    {
        while (true)
        {
            yield return null;
            Following();

            if (Input.GetMouseButtonUp(0))
            {
                EndFollow();
                break;
            }
        }
    }

    private void Following()
    {
        var position = ((IMovable)movingConnector).GetPosition();
        Vector3 distance_to_screen = Camera.main.WorldToScreenPoint(position);
        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen.z));
        ((IMovable)movingConnector).SetPosition(new Vector3(pos_move.x, position.y, pos_move.z));
    }

    public void EndFollow()
    {
        ((IMovable)movingConnector).EndMove();
    }
}
