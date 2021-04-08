using System.Collections;
using UnityEngine;

public class FollowFromMouseConnectorMover : MonoBehaviour
{
    [SerializeField]
    MovingConnector movingConnector;

    private Plane plane;

    // Start is called before the first frame update
    public void StartFollow()
    {
        ((IMovable)movingConnector).StartMove();
        var position = ((IMovable)movingConnector).GetPosition();
        plane = new Plane(Vector3.up, Vector3.up * position.y); // ground plane
        Following();
        StartCoroutine(StartFollowing());
    }

    private IEnumerator StartFollowing()
    {
        while(true)
        {
            yield return null;
            Following();

            if(Input.GetMouseButtonUp(0))
            {
                EndFollow();
                break;
            }
        }
    }

    private void Following()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance; // the distance from the ray origin to the ray intersection of the plane
        if (plane.Raycast(ray, out distance))
        {
            Vector3 rayPoint = ray.GetPoint(distance);
            Vector3 snappedRayPoint = rayPoint;

            ((IMovable)movingConnector).SetPosition(rayPoint);
        }
    }

    public void EndFollow()
    {
        ((IMovable)movingConnector).EndMove();
    }
}
