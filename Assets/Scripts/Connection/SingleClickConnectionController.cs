using System;
using System.Collections.Generic;
using UnityEngine;

public class SingleClickConnectionController
{
    private MovingConnection currentConnection;

    ConnectionPresenterFactory connectionPresenterFactory;
    ConnectorsSelectableGroup selectableGroup;
    ConnectorColors colors;

    public SingleClickConnectionController(IEnumerable<MovingConnector> connectors, ConnectionPresenterFactory connectionPresenterFactory, ConnectorsSelectableGroup selectableGroup, ConnectorColors colors)
    {
        this.colors = colors;
        this.selectableGroup = selectableGroup;
        this.connectionPresenterFactory = connectionPresenterFactory;
        foreach (var connector in connectors)
        {
            connector.OnConnectionPointClick += OnClick;
            connector.OnConnectionPointRetention += OnRetention;
            connector.OnDestroyConnector += OnDestroyConnector;
        }
    }

    private void OnDestroyConnector(MovingConnector connector)
    {
        connector.OnConnectionPointClick -= OnClick;
        connector.OnConnectionPointRetention -= OnRetention;
        connector.OnDestroyConnector -= OnDestroyConnector;
    }

    private void OnClick(MovingConnector connector)
    {
        if (currentConnection != null)
        {
            if (connector == currentConnection.GetConnector(currentConnection.GetConnectorCount() - 1))
            {
                currentConnection.Destroy();
            }
            else
            {
                currentConnection.AddConnector(connector);
            }

            currentConnection = null;
            selectableGroup.UnSelectAll();
        }
        else
        {
            Debug.Log("SingleClickConnection");
            selectableGroup.UnSelectAll();
            currentConnection = new MovingConnection(connector);
            connectionPresenterFactory.Create(currentConnection);
            connector.Select(colors.selectedColor);
            selectableGroup.SelectGroup(colors.whenSelectAnotherElementColor);
        }
    }

    private void OnRetention(MovingConnector connector)
    {
        if (currentConnection != null)
        {
            currentConnection.Destroy();
            currentConnection = null;
        }
    }
}
