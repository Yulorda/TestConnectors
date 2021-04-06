using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PointerReaderActionType
{
    Click,
    Enter,
    Exit,
    Up,
    Down
}

public class PointerReader : MonoBehaviour
{
    public event Action OnClick;
    public event Action OnEnter;
    public event Action OnExit;
    public event Action OnUp;
    public event Action OnDown;
    public event Action OnDrag;

    protected void Click()
    {
        OnClick?.Invoke();
    }

    protected void PointerEnter()
    {
        OnEnter?.Invoke();
    }

    protected void PointerExit()
    {
        OnExit?.Invoke();
    }

    protected void PointerUp()
    {
        OnUp?.Invoke();
    }

    protected void PointerDown()
    {
        OnDown?.Invoke();
    }

    protected void PointerDrag()
    {
        OnDrag?.Invoke();
    }
}