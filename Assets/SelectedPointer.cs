using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedPointer : MonoBehaviour
{
    [SerializeField]
    MovingConnector movingConnector;

    [SerializeField]
    ConnectorColors connectorColors;

    private MovingConnector currentConnector;

    private void Awake()
    {
        movingConnector.OnMoving += OnMoving;
    }

    private void OnDestroy()
    {
        movingConnector.OnMoving -= OnMoving;
    }

    private void OnMoving(MovingConnector obj)
    {
        var temp = FindConnector();

        if (currentConnector != null)
        {
            if (temp != currentConnector)
            {
                if (temp != movingConnector)
                {
                    currentConnector.Select(connectorColors.whenSelectAnotherElementColor);
                    currentConnector = temp;
                    if (currentConnector != null)
                    {
                        currentConnector.Select(connectorColors.selectedColor);
                    }
                }
                else
                {
                    currentConnector.Select(connectorColors.whenSelectAnotherElementColor);
                    currentConnector = null;
                }
            }
        }
        else
        {
            if (temp != movingConnector && temp != null)
            {
                currentConnector = temp;
                currentConnector.Select(connectorColors.selectedColor);
            }
        }
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
