using UnityEngine;

public interface IMovable
{
    void StartMove();
    void EndMove();
    Vector3 GetPosition();
    void SetPosition(Vector3 position);
}
