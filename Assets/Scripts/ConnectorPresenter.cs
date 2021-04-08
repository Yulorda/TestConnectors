using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorPresenter : MonoBehaviour
{
    [SerializeField]
    MovingConnector connector;

    [SerializeField]
    EmissionHighlighter highlighter;

    [SerializeField]
    ConnectorColors connectorColors;

    [SerializeField]
    PointerReader pointerReader;

    private void Awake()
    {
        pointerReader.OnDown += PointerDown;
        pointerReader.OnDrag += connector.ConnectionPointRetention;
        pointerReader.OnUp += connector.ConnectionPointUp;

        connector.GetSelectable().OnSelect += Select;
        connector.GetSelectable().OnSomeElementInGroupWasSelected += WhenSelectAnotherElement;
        connector.GetSelectable().OnUnSelect += UnSelect;
    }

    private void OnDestroy()
    {
        pointerReader.OnDown -= PointerDown;
        pointerReader.OnDrag -= connector.ConnectionPointRetention;
        pointerReader.OnUp -= connector.ConnectionPointUp;

        connector.GetSelectable().OnSelect -= Select;
        connector.GetSelectable().OnSomeElementInGroupWasSelected -= WhenSelectAnotherElement;
        connector.GetSelectable().OnUnSelect -= UnSelect;
    }

    public void PointerDown()
    {
        Debug.Log("PointerDown");
        connector.ConnectionPointClick();
    }

    public void PointerRetention()
    {

    }

    public void PointerUp()
    {

    }

    private void UnSelect()
    {
        highlighter.Highlight(false);
    }

    private void Select()
    {
        highlighter.color = connectorColors.selectedColor;
        highlighter.Highlight(true);
    }

    private void WhenSelectAnotherElement()
    {
        highlighter.color = connectorColors.whenSelectAnotherElementColor;
        highlighter.Highlight(true);
    }
}