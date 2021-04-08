using System;
using System.Collections;
using UnityEngine;

public class MovingConnector : MonoBehaviour, IMovable, ISelectable<Color>
{
    [SerializeField]
    Transform connectorPosition;

    private bool isSelected;
    public bool IsSelected => isSelected;

    public event Action<MovingConnector> OnStartMove;
    public event Action<MovingConnector> OnEndMove;
    public event Action<MovingConnector> OnMoving;

    public event Action<MovingConnector> OnConnectionPointClick;
    public event Action<MovingConnector> OnConnectionPointRetention;
    public event Action<MovingConnector> OnDestroyConnector;

    public event Action<Color> OnSelect;
    public event Action OnUnSelect;

    public void StartMove()
    {
        OnStartMove?.Invoke(this);
        StartCoroutine(MovingCoroutine());
    }

    public void EndMove()
    {
        OnEndMove?.Invoke(this);
        StopAllCoroutines();
    }

    IEnumerator MovingCoroutine()
    {
        while (true)
        {
            yield return null;
            OnMoving?.Invoke(this);
        }
    }

    public Vector3 GetConnectorPosition()
    {
        return connectorPosition.position;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void ConnectionPointDown()
    {
        OnConnectionPointClick?.Invoke(this);
    }

    public void ConnectionPointRetention()
    {
        OnConnectionPointRetention?.Invoke(this);
    }

    public void Select(Color value)
    {
        isSelected = true;
        OnSelect?.Invoke(value);
    }

    public void UnSelect()
    {
        isSelected = false;
        OnUnSelect?.Invoke();
    }

    public void OnDestroy()
    {
        OnDestroyConnector?.Invoke(this);
    }
}