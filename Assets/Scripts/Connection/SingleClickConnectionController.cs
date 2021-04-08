using System;
using System.Collections.Generic;
using UnityEngine;

public class SingleClickConnectionController
{
    private ConnectionPresenterFactory connectionPresenterFactory;
    private ConnectorsSelectableGroup selectableGroup;
    private ConnectorColors colors;

    private MovingConnection currentConnection;

    public SingleClickConnectionController(IEnumerable<MovingConnector> connectors, ConnectionPresenterFactory connectionPresenterFactory, ConnectorsSelectableGroup selectableGroup, ConnectorColors colors)
    {
        foreach (var connector in connectors)
        {
            connector.OnConnectionPointClick += OnClick;
            connector.OnConnectionPointRetention += OnRetention;
            connector.OnDestroyConnector += OnDestroyConnector;
        }

        this.connectionPresenterFactory = connectionPresenterFactory;
        this.colors = colors;
        this.selectableGroup = selectableGroup;
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
            if (connector == currentConnection.GetLastConnector())
            {
                currentConnection.Destroy();
            }
            else
            {
                currentConnection.AddConnector(connector);
            }

            selectableGroup.UnSelectAll();
            
            currentConnection = null;
        }
        else
        {
            Debug.Log("SingleClickConnection");
            
            selectableGroup.UnSelectAll();
         
            currentConnection = new MovingConnection(connector);
            connectionPresenterFactory.Create().Inject(currentConnection);

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
