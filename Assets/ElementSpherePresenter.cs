using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSpherePresenter : MonoBehaviour
{
    [SerializeField]
    ElementModel elementModel;
    [SerializeField]
    EmissionHighlighter highlighter;

    private void Awake()
    {
        elementModel.OnSelectSphere += OnSelectSphere;
        elementModel.OnUnSelectSphere += OnUnSelectSphere;
    }

    private void OnDestroy()
    {
        elementModel.OnSelectSphere -= OnSelectSphere;
        elementModel.OnUnSelectSphere -= OnUnSelectSphere;
    }

    private void OnUnSelectSphere()
    {
        highlighter.Highlight(false);
    }

    private void OnSelectSphere()
    {
        highlighter.Highlight(true);
    }
}
