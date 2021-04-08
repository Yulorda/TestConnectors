using System;
using System.Collections.Generic;
using UnityEngine;

public class SingleClickConnectionController
{
    private Connection currentConnection;

    ConnectionPresenterFactory connectionPresenterFactory;

    public SingleClickConnectionController(IEnumerable<IConnector> connectors, ConnectionPresenterFactory connectionPresenterFactory)
    {
        this.connectionPresenterFactory = connectionPresenterFactory;
        foreach (var connector in connectors)
        {
            connector.OnConnectionPointClick += OnClick;
            connector.OnConnectionPointRetention += OnRetention;
            connector.OnDestroyConnector += OnDestroyConnector;
        }
    }

    private void OnDestroyConnector(IConnector connector)
    {
        connector.OnConnectionPointClick -= OnClick;
        connector.OnConnectionPointRetention -= OnRetention;
        connector.OnDestroyConnector -= OnDestroyConnector;
    }

    private void OnClick(IConnector connector)
    {
        var selectable = connector.GetSelectable();

        Debug.Log("SingleClickConnection");

        if (currentConnection != null)
        {
            if (!currentConnection.TrySetConnectorEnd(connector))
            {
                currentConnection.Destroy();
            }
            currentConnection.ConnectionChangePosition();
            currentConnection = null;
            selectable.UnSelectGroup();
        }
        else
        {
            selectable.UnSelectGroup();
            currentConnection = new Connection(connector);
            connectionPresenterFactory.Create(currentConnection);
            selectable.Select();
        }
    }

    private void OnRetention(IConnector connector)
    {
        if (currentConnection != null)
        {
            currentConnection.Destroy();
            currentConnection = null;
        }
    }
}
