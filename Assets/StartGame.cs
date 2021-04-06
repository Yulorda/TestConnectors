using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    ElementModelFactory factory;

    [SerializeField]
    ConnectionController connectionController;

    [ContextMenu("Create")]
    public void Create()
    {
        var elements = factory.Create();
        SelectableController selectableController = new SelectableController(elements);
        connectionController.Inject(elements);
    }
}

public class ConnectionController : MonoBehaviour
{
    private IEnumerable<IConnector> connectors = new List<IConnector>();

    public void Inject(IEnumerable<IConnector> connectors)
    {
        this.connectors = connectors;
        foreach(var connector in connectors)
        {
        }
    }
}

public class Connection : IEquatable<Connection>
{
    public IConnector connector0;
    public IConnector connector1;

    public bool Equals(Connection other)
    {
        return connector0 == other.connector0 && connector1 == other.connector1;
    }

    public override int GetHashCode()
    {
        return connector0.GetHashCode() ^ connector1.GetHashCode();
    }
}

