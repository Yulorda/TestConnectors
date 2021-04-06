
using System;
using UnityEngine;

public interface IConnector
{
    event Action OnClick;
    event Action OnRetention;
    event Action OnActionEnd;

    Vector3 GetPosition();
    ISelectable GetSelectable();
}