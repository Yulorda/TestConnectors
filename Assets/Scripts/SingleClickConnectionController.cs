using System.Collections.Generic;
using UnityEngine;
using MovingConnector;

public class SingleClickConnectionController
{
    private ConnectionFactory connectionFactory;
    private ConnectorsSelectableGroup selectableGroup;
    private SelectableColors colors;

    private Connection currentConnection;

    public SingleClickConnectionController(IEnumerable<Connector> connectors, ConnectionFactory connectionFactory, ConnectorsSelectableGroup selectableGroup, SelectableColors colors)
    {
        foreach (var connector in connectors)
        {
            connector.OnConnectionPointPointerClick += OnClick;
            connector.OnConnectionPointPoinerDrag += OnRetention;
            connector.OnDestroy += OnDestroyConnector;
        }

        this.connectionFactory = connectionFactory;
        this.colors = colors;
        this.selectableGroup = selectableGroup;
    }

    private void OnDestroyConnector(Connector connector)
    {
        connector.OnConnectionPointPointerClick -= OnClick;
        connector.OnConnectionPointPoinerDrag -= OnRetention;
        connector.OnDestroy -= OnDestroyConnector;
    }

    private void OnClick(Connector connector)
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

            currentConnection = connectionFactory.Create(connector);

            connector.Select(colors.selectedColor);
            selectableGroup.SelectGroup(colors.whenSelectAnotherElementColor);
        }
    }

    private void OnRetention(Connector connector)
    {
        if (currentConnection != null)
        {
            currentConnection.Destroy();
            currentConnection = null;
        }
    }
}