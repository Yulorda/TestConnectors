using System;
using System.Collections.Generic;
using UnityEngine;

public class MovingConnection
{
    List<MovingConnector> connections = new List<MovingConnector>();

    public event Action<int, Vector3> OnConnectorMoving;
    public event Action OnDestroy;

    public MovingConnection(params MovingConnector[] connectors)
    {
        foreach (var connector in connectors)
        {
            AddConnector(connector);
        }
    }

    public void Destroy()
    {
        OnDestroy?.Invoke();

        foreach (var connector in connections)
        {
            UnSubscribeFromConnector(connector);
        }

        connections.Clear();
    }

    private void UnSubscribeFromConnector(MovingConnector connector)
    {
        connector.OnDestroyConnector -= Destroy;
        connector.OnMoving -= ConnectorChangePosition;
    }

    private void Destroy(MovingConnector connector)
    {
        Destroy();
    }

    public bool TryChangeConnector(MovingConnector newConnector, MovingConnector oldConnector)
    {
        if (connections.Contains(oldConnector))
        {
            UnSubscribeFromConnector(oldConnector);
            connections[connections.IndexOf(oldConnector)] = newConnector;
            Subscribe(newConnector);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddConnector(MovingConnector connector)
    {
        connections.Add(connector);
        Subscribe(connector);
    }

    private void Subscribe(MovingConnector connector)
    {
        connector.OnDestroyConnector += Destroy;
        connector.OnMoving += ConnectorChangePosition;
        ConnectorChangePosition(connector);
    }

    private void ConnectorChangePosition(MovingConnector connector)
    {
        OnConnectorMoving?.Invoke(connections.IndexOf(connector), connector.GetConnectorPosition());
    }

    public MovingConnector GetConnector(int index)
    {
        if (index < connections.Count)
        {
            return connections[index];
        }
        else
        {
            return null;
        }
    }

    public int GetConnectorCount()
    {
        return connections.Count;
    }
}