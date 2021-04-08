using System;

public class Connection
{
    public IConnector Connector0 { get => connector0; }
    public IConnector Connector1 { get => connector1; }

    public event Action OnConnectionChangePosition;
    public event Action OnDestroy;

    private IConnector connector0;
    private IConnector connector1;

    public Connection(IConnector connector0)
    {
        this.connector0 = connector0;

        connector0.OnDestroyConnector += Destroy;
    }

    public Connection(IConnector connector0, IConnector connector1)
    {
        this.connector0 = connector0;
        this.connector1 = connector1;

        connector0.OnDestroyConnector += Destroy;
        connector1.OnDestroyConnector += Destroy;
    }

    public void Destroy()
    {
        OnDestroy?.Invoke();

        if (connector0 != null)
            connector0.OnDestroyConnector -= Destroy;

        if (connector1 != null)
            connector1.OnDestroyConnector -= Destroy;
    }

    private void Destroy(IConnector connector)
    {
        Destroy();
    }

    public bool TrySetConnectorEnd(IConnector connector1)
    {
        if (connector0 != connector1)
        {
            connector1.OnDestroyConnector -= Destroy;

            this.connector1 = connector1;
            connector1.OnDestroyConnector += Destroy;

            return true;
        }
        return false;
    }

    public void ConnectionChangePosition()
    {
        OnConnectionChangePosition?.Invoke();
    }
}