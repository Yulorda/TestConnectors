using JetBrains.Annotations;
using UnityEngine;

public class MovingConnectionPresenter : MonoBehaviour
{
    [SerializeField]
    LineRenderer lineRenderer;

    private MovingConnection connection;

    public void Inject(MovingConnection connection)
    {
        this.connection = connection;
        connection.OnDestroy += OnDestroyConnection;
        connection.OnConnectorMoving += OnConnectionChangePosition;

        var length = connection.GetConnectorCount();
        if (length > 1)
        {
            for (int i = 0; i < connection.GetConnectorCount(); i++)
            {
                lineRenderer.SetPosition(i, connection.GetConnector(i).GetConnectorPosition());
            }
        }
        else
        {
            var point = connection.GetConnector(0).GetConnectorPosition();
            lineRenderer.SetPosition(0, point);
            lineRenderer.SetPosition(1, point);
        }
    }

    private void OnDestroy()
    {
        if (connection != null)
        {
            connection.OnConnectorMoving -= OnConnectionChangePosition;
            connection.OnDestroy -= OnDestroyConnection;
        }
    }

    private void OnDestroyConnection()
    {
        DestroyImmediate(this.gameObject);
    }

    private void OnConnectionChangePosition(int point, Vector3 pos)
    {
        lineRenderer.SetPosition(point, pos);
    }
}

