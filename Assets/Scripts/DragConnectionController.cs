using System.Collections.Generic;
using UnityEngine;
using MovingConnector;
using System;

public class DragConnectionController
{
    private ConnectionFactory connectionFactory;
    private ConnectorsSelectableGroup selectableGroup;
    private SelectableColors colors;

    private Connector fakeConnector;
    private FakeConnectorPresenter fakeConnectorPresenter;
    private Connection currentConnection;

    public DragConnectionController(IEnumerable<Connector> connectors,
        FakeConnectorPresenter fakeConnectorPresenter,
        ConnectionFactory connectionFactory,
        ConnectorsSelectableGroup selectableGroup,
        SelectableColors colors)
    {
        foreach (var connector in connectors)
        {
            connector.OnConnectionPointPoinerDrag += OnRetention;
            connector.OnConnectionPointPointerClick += OnClick;
            connector.OnDestroy += OnDestroyConnector;
        }

        Func<Vector3> getFakeConnectorPosition = () => fakeConnectorPresenter.followMouse.transform.position;
        Action<Vector3> setFaleConnectorPosition = (x) => fakeConnectorPresenter.followMouse.transform.position = x;

        fakeConnector = new Connector(getFakeConnectorPosition, getFakeConnectorPosition, setFaleConnectorPosition);
        fakeConnectorPresenter.Inject(fakeConnector, selectableGroup, colors);

        this.connectionFactory = connectionFactory;
        this.selectableGroup = selectableGroup;
        this.colors = colors;
        this.fakeConnectorPresenter = fakeConnectorPresenter;

        //TODO: unsubscribe
        fakeConnector.OnConnectionPointPointerDragEnd += OnEndMoveFakeConnector;
    }

    private void OnDestroyConnector(Connector connector)
    {
        connector.OnConnectionPointPoinerDrag -= OnRetention;
        connector.OnConnectionPointPointerClick -= OnClick;
        connector.OnDestroy -= OnDestroyConnector;
    }

    private void OnClick(Connector connector)
    {
        fakeConnector.SetPosition(connector.GetConnectorPosition());
    }

    private void OnRetention(Connector connector)
    {
        if (currentConnection == null)
        {
            currentConnection = connectionFactory.Create(connector, fakeConnector);

            fakeConnectorPresenter.followMouse.StartFollow();
            fakeConnectorPresenter.selectedPointer.ChangeSelectedState(true);
            fakeConnectorPresenter.selectedPointer.AddToIgnoreList(connector);

            connector.Select(colors.selectedColor);
            selectableGroup.SelectGroup(colors.whenSelectAnotherElementColor);
        }
    }

    private void OnEndMoveFakeConnector(Connector connector)
    {
        var temp = FindConnector();

        fakeConnectorPresenter.selectedPointer.ChangeSelectedState(false);
        fakeConnectorPresenter.selectedPointer.RemoveFromIgnoreList(currentConnection.GetConnector(0));

        if (temp != null && temp != currentConnection.GetConnector(0))
        {
            if (!currentConnection.TryChangeConnector(temp, fakeConnector))
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

    private Connector FindConnector()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                var connectorPresenter = hit.collider.gameObject.GetComponent<ConnectionPointPresenter>();

                if (connectorPresenter != null && selectableGroup.Contains(connectorPresenter.connector))
                {
                    return connectorPresenter.connector;
                }
            }
        }
        return null;
    }
}