using UnityEngine;

public class ConnectionPresenter : MonoBehaviour
{
    [SerializeField]
    LineRenderer lineRenderer;

    private Connection connection;

    public void Inject(Connection connection)
    {
        this.connection = connection;
        connection.OnDestroy += OnDestroyConnection;
        connection.OnConnectionChangePosition += OnConnectionChangePosition;
    }

    private void OnDestroy()
    {
        if (connection != null)
        {
            connection.OnDestroy -= OnDestroyConnection;
            connection.OnConnectionChangePosition -= OnConnectionChangePosition;
        }
    }

    private void OnDestroyConnection()
    {
        DestroyImmediate(this.gameObject);
    }

    public void OnConnectionChangePosition()
    {
        lineRenderer.SetPosition(0, connection.Connector0.GetConnectorPosition());
        lineRenderer.SetPosition(1, connection.Connector1.GetConnectorPosition());
    }
}

