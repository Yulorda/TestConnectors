using System;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    public event Action<int, Vector3> OnConnectorMoving;
    public event Action OnDestroy;

    List<Connector> connections = new List<Connector>();

    public Connection(params Connector[] connectors)
    {
        foreach (var connector in connectors)
        {
            AddConnector(connector);
        }
    }

    public void AddConnector(Connector connector)
    {
        connections.Add(connector);
        Subscribe(connector);
    }

    private void Subscribe(Connector connector)
    {
        connector.OnDestroy += Destroy;
        connector.OnMoving += ConnectorChangePosition;
        ConnectorChangePosition(connector);
    }

    private void UnSubscribeFromConnector(Connector connector)
    {
        connector.OnDestroy -= Destroy;
        connector.OnMoving -= ConnectorChangePosition;
    }

    public bool TryChangeConnector(Connector newConnector, Connector oldConnector)
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

    public Connector GetConnector(int index)
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

    public Connector GetLastConnector()
    {
        return connections[connections.Count - 1];
    }

    public int GetConnectorCount()
    {
        return connections.Count;
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

    private void Destroy(Connector connector)
    {
        Destroy();
    }

    private void ConnectorChangePosition(Connector connector)
    {
        OnConnectorMoving?.Invoke(connections.IndexOf(connector), connector.GetConnectorPosition());
    }
}