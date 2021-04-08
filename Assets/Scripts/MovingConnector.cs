using System;
using System.Collections;
using UnityEngine;

public class MovingConnector : MonoBehaviour, IMovable, IConnector
{
    [SerializeField]
    SelectableGroupElemenet selectableGroupElement;

    [SerializeField]
    Transform connectorPosition;

    public event Action OnStartMove;
    public event Action OnEndMove;
    public event Action Moving;

    public event Action<IConnector> OnConnectionPointClick;
    public event Action<IConnector> OnConnectionPointRetention;
    public event Action<IConnector> OnConnectionPointUp;
    public event Action<IConnector> OnDestroyConnector;

    public void StartMove()
    {
        OnStartMove?.Invoke();
        StartCoroutine(MovingCoroutine());
    }

    public void EndMove()
    {
        OnEndMove?.Invoke();
        StopAllCoroutines();
    }

    IEnumerator MovingCoroutine()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
            Moving?.Invoke();
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

    public SelectableGroupElemenet GetSelectable()
    {
        return selectableGroupElement;
    }

    public void ConnectionPointClick()
    {
        OnConnectionPointRetention?.Invoke(this);
    }

    public void ConnectionPointUp()
    {
        OnConnectionPointUp?.Invoke(this);
    }

    public void ConnectionPointRetention()
    {
        OnConnectionPointClick?.Invoke(this);
    }

    public void OnDestroy()
    {
        OnDestroyConnector?.Invoke(this);
    }
}