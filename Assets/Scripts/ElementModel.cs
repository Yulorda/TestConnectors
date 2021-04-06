using System;
using UnityEngine;

public class ElementModel : MonoBehaviour, IMovable, ISelectable, IConnector
{
    public event Action OnStartMove;
    public event Action OnEndMove;

    public event Action OnSelectSphere;
    public event Action OnUnSelectSphere;

    private event Action OnSphereClick;
    private event Action OnSphereRetention;
    private event Action OnSphereActionEnd;

    event Action IConnector.OnClick
    {
        add
        {
            OnSphereClick += value;
        }

        remove
        {
            OnSphereClick -= value;
        }
    }

    event Action IConnector.OnRetention
    {
        add
        {
            OnSphereRetention += value;
        }

        remove
        {
            OnSphereRetention -= value;
        }
    }

    event Action IConnector.OnActionEnd
    {
        add
        {
            OnSphereActionEnd += value;
        }

        remove
        {
            OnSphereActionEnd -= value;
        }
    }

    private bool selectState;
    bool ISelectable.IsSelected => selectState;
   
    void IMovable.StartMove()
    {
        OnStartMove?.Invoke();
    }

    void IMovable.EndMove()
    {
        OnEndMove?.Invoke();
    }

    Vector3 IMovable.GetPosition()
    {
        return transform.position;
    }

    void IMovable.SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    void ISelectable.Select()
    {
        OnSelectSphere?.Invoke();
    }

    void ISelectable.UnSelect()
    {
        OnUnSelectSphere?.Invoke();
    }

    ISelectable IConnector.GetSelectable()
    {
        return this;
    }

    Vector3 IConnector.GetPosition()
    {
        return transform.position;
    }
}