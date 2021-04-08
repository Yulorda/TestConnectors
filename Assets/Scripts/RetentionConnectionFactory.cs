using System;
using System.Collections.Generic;
using UnityEngine;

public class RetentionConnectionFactory
{
    public event Action<Connection> OnConnectionCreate;

    private Connection currentConnection;

    private MovingConnector fakeConnector;
    private PointerReader fakeConnectorPointerReader;

    public RetentionConnectionFactory(IEnumerable<IConnector> connectors, MovingConnector fakeConnector, PointerReader fakeConnectorPointerReader)
    {
        this.fakeConnector = fakeConnector;
        this.fakeConnectorPointerReader = fakeConnectorPointerReader;
        foreach (var connector in connectors)
        {
            connector.OnConnectionPointRetention += OnRetention;
            connector.OnConnectionPointUp += OnPointerUp;
            connector.OnDestroyConnector += OnDestroyConnector;
        }
    }

    private void OnDestroyConnector(IConnector connector)
    {
        connector.OnConnectionPointRetention -= OnRetention;
        connector.OnConnectionPointUp -= OnPointerUp;
        connector.OnDestroyConnector -= OnDestroyConnector;
    }

    private void OnRetention(IConnector connector)
    {
        if (currentConnection == null)
        {
            currentConnection = new Connection(connector, fakeConnector);
            OnConnectionCreate?.Invoke(currentConnection);
            var selectable = connector.GetSelectable();
            selectable.UnSelectGroup();
            selectable.Select();
            fakeConnectorPointerReader.PointerDown();
        }

        fakeConnectorPointerReader.PointerDrag();
        currentConnection.ConnectionChangePosition();

        var temp = FindConnector();

        if(temp!=null)
        {
            var selectable = temp.GetSelectable();
            selectable.Select();
        }
    }

    private IConnector FindConnector()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                var connector = hit.collider.gameObject.GetComponent<IConnector>();

                if (connector != null)
                {
                    return connector;
                }
            }
        }
        return null;
    }

    private void OnPointerUp(IConnector connector)
    {
        if (currentConnection != null)
        {
            fakeConnectorPointerReader.PointerUp();

            var temp = FindConnector();

            if (temp == null && !currentConnection.TrySetConnectorEnd(temp))
                currentConnection.Destroy();

            currentConnection = null;
        }
    }
}