using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementMovePresenter : MonoBehaviour
{
    [SerializeField]
    ElementModel elementModel;

    [SerializeField]
    EmissionHighlighter highlighter;

    private void Awake()
    {
        elementModel.OnStartMove += OnStartMove;
        elementModel.OnEndMove += OnEndMove;
    }

    private void OnDestroy()
    {
        elementModel.OnStartMove -= OnStartMove;
        elementModel.OnEndMove -= OnEndMove;
    }

    private void OnEndMove()
    {
        highlighter.Highlight(false);
    }

    private void OnStartMove()
    {
        highlighter.Highlight(true);
    }
}
