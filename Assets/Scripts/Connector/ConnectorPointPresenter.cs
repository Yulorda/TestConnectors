using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorPointPresenter : MonoBehaviour
{
    public MovingConnector connector;

    [SerializeField]
    EmissionHighlighter highlighter;

    private void Awake()
    {
        connector.OnSelect += Select;
        connector.OnUnSelect += UnSelect;
    }

    private void OnDestroy()
    {
        connector.OnSelect -= Select;
        connector.OnUnSelect -= UnSelect;
    }

    private void OnMouseDown()
    {
        Debug.Log("PointerDown");
        connector.ConnectionPointDown();
    }

    private void OnMouseExit()
    {
        if (Input.GetMouseButton(0) && connector.IsSelected)
        {
            Debug.Log("ConnectionPointRetention");
            connector.ConnectionPointRetention();
        }
    }

    private void UnSelect()
    {
        highlighter.Highlight(false);
    }

    private void Select(Color color)
    {
        highlighter.color = color;
        highlighter.Highlight(true);
    }
}