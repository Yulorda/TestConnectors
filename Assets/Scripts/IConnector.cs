
using System;
using UnityEngine;

public interface IConnector
{
    event Action<IConnector> OnConnectionPointClick;
    event Action<IConnector> OnConnectionPointRetention;
    event Action<IConnector> OnConnectionPointUp;
    event Action<IConnector> OnDestroyConnector;

    Vector3 GetConnectorPosition();
    SelectableGroupElemenet GetSelectable();
}