using System;
using UnityEngine;

public class PointerReader : MonoBehaviour
{
    public event Action OnClick;
    public event Action OnEnter;
    public event Action OnExit;
    public event Action OnUp;
    public event Action OnDown;
    public event Action OnDrag;

    public void Click()
    {
        OnClick?.Invoke();
    }

    public void PointerEnter()
    {
        OnEnter?.Invoke();
    }

    public void PointerExit()
    {
        OnExit?.Invoke();
    }

    public void PointerUp()
    {
        OnUp?.Invoke();
    }

    public void PointerDown()
    {
        OnDown?.Invoke();
    }

    public void PointerDrag()
    {
        OnDrag?.Invoke();
    }
}