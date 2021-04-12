using System.Collections.Generic;
using UnityEngine;

namespace MovingConnector
{
    public class SelectedPointer : MonoBehaviour
    {
        private Connector connector;
        private ConnectorsSelectableGroup selectableGroup;
        private SelectableColors colors;
        private Connector currentConnector;

        private List<Connector> ignore = new List<Connector>();

        public void Inject(Connector connector, ConnectorsSelectableGroup selectableGroup, SelectableColors colors)
        {
            this.connector = connector;
            this.selectableGroup = selectableGroup;
            this.colors = colors;

            connector.OnDestroy += Destroy;
        }

        public void ChangeSelectedState(bool state)
        {
            if (state)
            {
                connector.OnMoving += OnMoving;
            }
            else
            {
                connector.OnMoving -= OnMoving;
            }
        }

        public void AddToIgnoreList(Connector connector)
        {
            if(!ignore.Contains(connector))
            {
                ignore.Add(connector);
            }
        }

        public void RemoveFromIgnoreList(Connector connector)
        {
            ignore.Remove(connector);
        }

        private void OnMoving(Connector obj)
        {
            var temp = FindConnector();

            //TODO: fix unselect main connector
            if (currentConnector != null)
            {
                if (temp != currentConnector)
                {
                    if (temp != connector)
                    {
                        currentConnector.Select(colors.whenSelectAnotherElementColor);
                        currentConnector = temp;
                        if (currentConnector != null)
                        {
                            currentConnector.Select(colors.selectedColor);
                        }
                    }
                    else
                    {
                        currentConnector.Select(colors.whenSelectAnotherElementColor);
                        currentConnector = null;
                    }
                }
            }
            else
            {
                if (temp != connector && temp != null && !ignore.Contains(temp))
                {
                    currentConnector = temp;
                    currentConnector.Select(colors.selectedColor);
                }
            }
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

        private void OnDestroy()
        {
            connector.OnMoving -= OnMoving;
        }

        private void Destroy(Connector connector)
        {
            DestroyImmediate(this.gameObject);
        }
    }
}
