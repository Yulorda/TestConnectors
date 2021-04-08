using System;
using UnityEngine;

public class SelectableGroupElemenet : MonoBehaviour, ISelectable
{
    private bool isSelected;

    public event Action OnSelect;
    public event Action OnSomeElementInGroupWasSelected;
    public event Action OnUnSelect;

    public event Action<SelectableGroupElemenet> SelectableGroupEvent;
    public event Action UnSelectableGroupEvent;

    public void Select()
    {
        isSelected = true;
        OnSelect?.Invoke();
        SelectableGroupEvent?.Invoke(this);
    }

    public void UnSelect()
    {
        isSelected = false;
        OnUnSelect?.Invoke();
    }

    public void SomeElementInGroupWasSelected()
    {
        if (!isSelected)
            OnSomeElementInGroupWasSelected?.Invoke();
    }

    public void UnSelectGroup()
    {
        UnSelectableGroupEvent?.Invoke();
    }
}