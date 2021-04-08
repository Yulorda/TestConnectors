﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetentionConnectionController
{
    private MovingConnection currentConnection;

    private MovingConnector fakeConnector;
    private FollowFromMouseConnectorMover followFromMouseConnectorMover;
    private ConnectionPresenterFactory connectionPresenterFactory;
    private ConnectorsSelectableGroup selectableGroup;
    private ConnectorColors colors;

    public RetentionConnectionController(IEnumerable<MovingConnector> connectors, 
        MovingConnector fakeConnector, 
        FollowFromMouseConnectorMover followFromMouseConnectorMover, 
        ConnectionPresenterFactory connectionPresenterFactory,
        ConnectorsSelectableGroup selectableGroup,
        ConnectorColors colors)
    {
        this.colors = colors;
        this.selectableGroup = selectableGroup;
        this.fakeConnector = fakeConnector;
        this.connectionPresenterFactory = connectionPresenterFactory;
        fakeConnector.OnEndMove += OnEndMoveFakeConnector;
        this.followFromMouseConnectorMover = followFromMouseConnectorMover;
        foreach (var connector in connectors)
        {
            connector.OnConnectionPointRetention += OnRetention;
            connector.OnDestroyConnector += OnDestroyConnector;
        }
    }

    private void OnDestroyConnector(MovingConnector connector)
    {
        connector.OnConnectionPointRetention -= OnRetention;
        connector.OnDestroyConnector -= OnDestroyConnector;
    }

    private void OnRetention(MovingConnector connector)
    {
        if (currentConnection == null)
        {
            currentConnection = new MovingConnection(connector, fakeConnector);
            followFromMouseConnectorMover.StartFollow();
            connector.Select(colors.selectedColor);
            selectableGroup.SelectGroup(colors.whenSelectAnotherElementColor);
            connectionPresenterFactory.Create(currentConnection);
        }
    }

    private void OnEndMoveFakeConnector(MovingConnector obj)
    {
        var temp = FindConnector();

        if(temp!=null && temp!= currentConnection.GetConnector(0))
        {
            if(!currentConnection.TryChangeConnector(temp,fakeConnector))
            {
                currentConnection.Destroy();
            }
        }
        else
        {
            currentConnection.Destroy();
        }

        selectableGroup.UnSelectAll();

        currentConnection = null;
    }
   
    private MovingConnector FindConnector()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                var connectorPresenter = hit.collider.gameObject.GetComponent<ConnectorPointPresenter>();

                if (connectorPresenter != null)
                {
                    return connectorPresenter.connector;
                }
            }
        }
        return null;
    }
}