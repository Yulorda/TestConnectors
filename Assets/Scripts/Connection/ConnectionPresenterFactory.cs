using UnityEngine;

public class ConnectionPresenterFactory : MonoBehaviour
{
    [SerializeField]
    private MovingConnectionPresenter connectionLinePresenterPrefab;

    public void Create(MovingConnection connection)
    {
        var newConnectionPresenter = Instantiate(connectionLinePresenterPrefab, transform);
        newConnectionPresenter.Inject(connection);
    }
}