using System.Linq;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    int count;

    [SerializeField]
    Main main;

    [SerializeField]
    ConnectorFactory connectionPrefabFactory;

    [SerializeField]
    ConnectionPresenterFactory connectionPresenterFactory;

    [SerializeField]
    MovingConnector fakeConnector;

    [SerializeField]
    FollowFromMouseConnectorMover followFromMouseConnectorMover;

    [SerializeField]
    ConnectorColors colors;

    [ContextMenu("Create")]
    public void Create()
    {
        var elements = connectionPrefabFactory.Create(count);

        foreach (var element in elements)
        {
            element.SetPosition(GetPointInСircle.GetCoodinates(main.Radius));
        }

        ConnectorsSelectableGroup selectableController = new ConnectorsSelectableGroup(elements);

        var singleClickConnectionController = new SingleClickConnectionController(elements, connectionPresenterFactory, selectableController, colors);
        var retentionConnectionFactory = new RetentionConnectionController(elements, fakeConnector, followFromMouseConnectorMover, connectionPresenterFactory, selectableController, colors);
    }
}