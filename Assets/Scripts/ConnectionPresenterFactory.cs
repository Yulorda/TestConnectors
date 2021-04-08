using UnityEngine;

public class ConnectionPresenterFactory : MonoBehaviour
{
    [SerializeField]
    private ConnectionPresenter connectionLinePresenterPrefab;

    public void Create(Connection connection)
    {
        var newConnectionPresenter = Instantiate(connectionLinePresenterPrefab, transform);
        newConnectionPresenter.Inject(connection);
    }
}